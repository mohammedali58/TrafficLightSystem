import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { SignalrService } from '../services/signalr.service';
import { TrafficLightState } from '../models/traffic-light-state';
import { TrafficLightOptions } from '../models/traffic-light-options';

@Component({
  selector: 'app-traffic-light-component',
  templateUrl: './traffic-light.component.html'
})
export class TrafficLightComponent implements OnInit {

  trafficLightState: TrafficLightState = {
    color: 'Red',
    secondsElapsed: 0
  };
  baseUrl: string = 'https://localhost:7122';
  trafficLightOptions: TrafficLightOptions = null!;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    private signalrService: SignalrService) { }

  ngOnInit(): void {
    this.signalrService.startConnection();
    this.signalrService.addTrafficLightListner(this.handleTrafficLightChange);
    this.getCurrentConfiguration();
  }

  startTrafficLight() {
    this.http.get<void>(this.baseUrl + '/api/TrafficLight' + '/Start')
      .subscribe(
        () => {
          console.log('Traffic Light Started!');

        },
        (err) => { })
  }

  reset() {
    this.http.get<void>(this.baseUrl + '/api/TrafficLight' + '/Reset')
      .subscribe(
        () => {

        },
        (err) => { })
  }

  handleTrafficLightChange = (trafficLightState: TrafficLightState) => {
    console.log(trafficLightState);
    this.trafficLightState = trafficLightState;
  }

  pushPadestrianButton() {
    this.http.get<void>(this.baseUrl + '/api/Trafficlight' + '/PushPadestrianButton')
      .subscribe(
        () => {
          console.log('PadestrianButton Pushed!');
        },
        (err) => { })
  }

  getCurrentConfiguration() {
    this.http.get<TrafficLightOptions>(this.baseUrl + '/api/Trafficlight' + '/GetCurrentConfiguration')
      .subscribe(
        (res) => {
          this.trafficLightOptions = res;
        },
        (err) => { })
  }

  get imageUrl(): string {
    return this.trafficLightState ? `/assets/images/${this.trafficLightState.color}.png` : ''
  }
}
