import * as SignalR from "@microsoft/signalr";

export const useWebSockets = (): WebSockets => {
    return new WebSockets();
};

class WebSockets {
    connection: SignalR.HubConnection;

    constructor() {
        this.connection = new SignalR.HubConnectionBuilder()
            .withUrl(`${process.env.REACT_APP_BACKEND}/api/websocket`, SignalR.HttpTransportType.WebSockets)
            .configureLogging(SignalR.LogLevel.Information)
            .withAutomaticReconnect()
            .build();
    }

    registerHandler(name: string, callback: any) {
        console.log(`WebSocket handler registered: ${name}...`);
        this.connection.on(name, (data: string) => callback(data));
    }

    startConnection(): Promise<void> | undefined {
        if (this.connection.state !== SignalR.HubConnectionState.Connected) {
            return this.connection.start();
        }

        console.log("WebSockets cannot be started. Using auxiliary HTTP status requests...");
        return undefined;
    }

    stopConnection(): Promise<void> | undefined {
        if (this.connection.state === SignalR.HubConnectionState.Connected) {
            return this.connection.stop();
        }

        return undefined;
    }
}
