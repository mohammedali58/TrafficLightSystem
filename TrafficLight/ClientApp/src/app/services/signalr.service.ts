import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { TrafficLightState } from '../models/traffic-light-state';
import { TrafficLightService } from './traffic-light.service';


@Injectable({
    providedIn: 'root'
})
export class SignalrService {

    private hubConnection!: signalR.HubConnection;

    constructor() { }

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:7122/Notify', {
                skipNegotiation: true,
                transport: signalR.HttpTransportType.WebSockets
            })
            .build();
        this.hubConnection
            .start()
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public addTrafficLightListner = (callbackFn: (trafficLightState: TrafficLightState) => void) => {
        this.hubConnection.on('Send', callbackFn);
    }
}