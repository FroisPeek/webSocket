using System.Net.WebSockets;
using System.Text;

public class WebSocketService
{
    private readonly List<WebSocket> _connections = new();

    public async Task HandleConnectionAsync(WebSocket ws, string name)
    {
        _connections.Add(ws);

        await Broadcast($"{name} joined the room.");
        await Broadcast($"{_connections.Count} connections");

        await ReceiveMessageAsync(ws, async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                await Broadcast($"{name} : {message}");
            }
            else if (result.MessageType == WebSocketMessageType.Close || ws.State == WebSocketState.Aborted)
            {
                _connections.Remove(ws);
                await Broadcast($"{name} left the room.");
                await Broadcast($"{_connections.Count} connections");
                await ws.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            }
        });
    }

    private async Task Broadcast(string message)
    {
        var bytes = Encoding.UTF8.GetBytes(message);
        foreach (var socket in _connections)
        {
            if (socket.State == WebSocketState.Open)
            {
                var array = new ArraySegment<byte>(bytes);
                await socket.SendAsync(array, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

    private async Task ReceiveMessageAsync(WebSocket websocket, Action<WebSocketReceiveResult, byte[]> handleMessage)
    {
        var buffer = new byte[1024 * 4];
        while (websocket.State == WebSocketState.Open)
        {
            var result = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            handleMessage(result, buffer);
        }
    }
}