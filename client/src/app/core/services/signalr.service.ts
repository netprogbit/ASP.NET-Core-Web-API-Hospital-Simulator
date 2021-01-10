import { Injectable, OnDestroy } from '@angular/core';
import { Store } from '@ngrx/store';
import * as signalR from "@aspnet/signalr";
import { environment } from './../../../environments/environment';
import { IAppState } from '../store/app/app.state';
import { ChangeMeasurement } from '../store/device/device.actions';
import { IMeasurement } from '../models/measurement.interface';

// The service lives while the product-detail component lives
// This is required to reset a hub connection
@Injectable()
export class SignalrService implements OnDestroy {

    private hubConnection: signalR.HubConnection;
    private auctionHubUrl = environment.apiUrl + environment.apiHospital;
    private deviceId: string;

    constructor(private store: Store<IAppState>) { }

    public startConnection = () => {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(this.auctionHubUrl)
            .build();

        this.hubConnection
            .start()
            .then(() => {
                console.log('Hub Connection Started.');
            })
    }

    public addTransferStatusDataListener = (deviceId: number) => {
        this.deviceId = deviceId.toString();
        this.hubConnection.on(this.deviceId, (jsonMessage) => {
            const measurement: IMeasurement = JSON.parse(jsonMessage);
            this.store.dispatch(new ChangeMeasurement(measurement));
        });
    }

    ngOnDestroy() {
        this.hubConnection.off(this.deviceId);
    }
}
