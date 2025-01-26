# Chat WebSocket Server

Este projeto é um servidor de **WebSocket** para criar um chat simples onde vários clientes podem se conectar, enviar mensagens e receber atualizações em tempo real.

---

## Configuração do Servidor

### Pré-requisitos
- [.NET 7 ou superior](https://dotnet.microsoft.com/download)
- IDE como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/)

### Como executar
1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-repositorio/chat-websocket.git
   cd chat-websocket

2. Coloque no console da página: 
   ```bash
   var ws = new WebSocket('ws://localhost:5000/ws?name=SeuNome');
