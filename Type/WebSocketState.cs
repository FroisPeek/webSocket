namespace wsapi.Type;

public enum WebSocketState
{
    none = 0,
    connectiong = 1, 
    open = 2,
    closeSent = 3,
    closeReceived = 4,
    closed = 5,
    error = 6,
}