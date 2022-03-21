import { Injectable } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
} from '@microsoft/signalr';
import { EMPTY, from, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IServerMethod } from './method-interfaces/server-method.interface';

@Injectable()
export class SignalrGeneralService {
  private hubConnection: HubConnection | null = null;

  public createConnection(
    path: string,
    automaticReconnectIntervals: number[] = [0, 2, 10, 30]
  ): void {
    if (
      !this.hubConnection ||
      this.hubConnection.state == HubConnectionState.Disconnected
    )
      this.hubConnection = new HubConnectionBuilder()
        .withAutomaticReconnect(automaticReconnectIntervals)
        .withUrl(environment.serverAddress + path, { withCredentials: true })
        .build();
  }

  public startConnection(): Observable<void> {
    if (
      this.hubConnection &&
      this.hubConnection.state == HubConnectionState.Disconnected
    )
      return from(this.hubConnection.start());
    return EMPTY;
  }

  public endConnection(): Observable<void> {
    if (
      this.hubConnection &&
      this.hubConnection.state == HubConnectionState.Connected
    )
      return from(this.hubConnection.stop());
    return EMPTY;
  }

  public addOnReconnectedHandler(callback: () => void): void {
    this.hubConnection?.onreconnected(callback);
  }

  public addOnReconnectingHandler(callback: () => void): void {
    this.hubConnection?.onreconnecting(callback);
  }

  public invokeServerMethod<T>(method: IServerMethod): Observable<T> {
    if (this.hubConnection)
      return from(
        this.hubConnection?.invoke<T>(method.methodName, ...method.args)
      );
    return EMPTY;
  }

  public sendMessageToServer(method: IServerMethod): Observable<void> {
    if (this.hubConnection)
      from(this.hubConnection.send(method.methodName, ...method.args));
    return EMPTY;
  }

  public addOnCloseHandler(callback: () => void): void {
    this.hubConnection?.onclose(callback);
  }

  public addOnServerMethodHandler(
    method: IServerMethod,
    handler: (...args: any[]) => void
  ): void {
    this.hubConnection?.on(method.methodName, handler);
  }

  public removeOnServerMethodHandler(method: IServerMethod): void {
    this.hubConnection?.off(method.methodName);
  }

  public setKeepAliveAndServerTimeout(
    keepAliveIntervalInMilliseconds: number = 15000,
    serverTimeoutInMilliseconds: number = 30000
  ) {
    if (this.hubConnection) {
      this.hubConnection.keepAliveIntervalInMilliseconds =
        keepAliveIntervalInMilliseconds;
      this.hubConnection.serverTimeoutInMilliseconds =
        serverTimeoutInMilliseconds;
    }
  }
}
