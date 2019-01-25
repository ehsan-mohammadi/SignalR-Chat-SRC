# SignalR Chat (SRC)

> A console chat application using SignalR

<img src="https://github.com/ehsan-mohammadi/SignalR-Chat-SRC/blob/master/Images/LogoSRC.png" width="120" height="120" />

SRC is an open source and free application to anonymous chat with other online users. SRC based on SignalR technology. This project consist of server and client which are connected using the WebSocket.

## Server side

Server is a console application to manage clients, control and store data. The [SRCHub](https://github.com/ehsan-mohammadi/SignalR-Chat-SRC/blob/master/SRC%20Server/Hubs/SRCHub.cs) use to call methods on connected clients from the server.

## Client side

In the client, users can join and chat with other online users. The [SRCClientCore](https://github.com/ehsan-mohammadi/SignalR-Chat-SRC/blob/master/SRC%20Client/SRCClientCore.cs) use to call methods on each client.

![ImageSRC](https://github.com/ehsan-mohammadi/SignalR-Chat-SRC/blob/master/Images/ImageSRC1.png)

## Instructions

| Command     | Description          |
| ---         | ---
| `/join`     | Join to the server   |
| `/set`      | Take a name for you  |
| `/onlines`  | Get all online users |
| `/leave`    | Leave the server     |
| `/clear`    | Clear the screen     |
| `/quit`     | Quit from SRC        |

## Getting started

If you want to get the source of SRC, just use `git clone "https://github.com/ehsan-mohammadi/SignalR-Chat-SRC.git"` and open the project in Visual Studio.

## Join the server

Open **SRC Server.exe**. then open **SRC Client.exe** and type the following instruction:

- `/set`
- `Pick a name: [Your name]`
- `/join`

And start chat. Also, to disconnect from the server just type:

- `/leave`

## License

SRC  is licensed under the [MIT © Ehsan Mohammadi.](../master/LICENSE)
