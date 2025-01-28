using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using System.Text;

[ApiController]
[Route("ws")]
public class WebSocketController : ControllerBase
{
    private readonly WebSocketService _webSocketService;

    public WebSocketController(WebSocketService webSocketService)
    {
        _webSocketService = webSocketService;
    }

    [HttpGet]
    public async Task<IActionResult> HandleWebSocket()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var currentName = HttpContext.Request.Query["name"];
            using var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await _webSocketService.HandleConnectionAsync(ws, currentName);
            return new EmptyResult();
        }
        else
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return BadRequest("This endpoint requires a WebSocket connection.");
        }
    }
}