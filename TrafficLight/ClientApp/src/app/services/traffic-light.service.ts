import { Inject, Injectable } from "@angular/core";
import { HttpClient } from "@microsoft/signalr";
import { TrafficLightState } from "../models/traffic-light-state";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class TrafficLightService {

    constructor(
        private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) { }

    public getCurrentTrafficLightState() {
        return this.http.get(this.baseUrl + 'GetCurrentTrafficLightState')

    }


}