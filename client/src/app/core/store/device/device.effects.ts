import { Effect, ofType, Actions } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import {
    GetDevices,
    GetDevicesSuccess,
    AddDevice,
    AddDeviceSuccess,
    SubmitDevice,            
    SubmitDeviceSuccess,
    DeleteDevice,    
    DeviceActionTypes
} from './device.actions';
import { of } from 'rxjs';
import { 
    switchMap, 
    map, 
    tap 
} from 'rxjs/operators';
import { DeviceService } from '../../services/device.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IDevice } from '../../models/device.interface';

@Injectable()
export class DeviceEffects {

    @Effect()
    getDevices$ = this.actions$.pipe(
        ofType<GetDevices>(DeviceActionTypes.GetDevices),        
        switchMap(() => {
            return this.deviceService.getDevices();
        }),
        switchMap((devices: IDevice[]) => {
            return of(new GetDevicesSuccess(devices));
        })
    );
    
    @Effect()
    addDevice$ = this.actions$.pipe(
        ofType<AddDevice>(DeviceActionTypes.AddDevice),
        map(action => action.payload),
        switchMap(payload => {
            return this.deviceService.add(payload.id, payload.serialNumber, payload.name);
        }),
        switchMap((data: any) => {
            return [new AddDeviceSuccess(data.message), new GetDevices()];
        })
    );

    @Effect({ dispatch: false })
    addDeviceSuccess$ = this.actions$.pipe(
        ofType<AddDeviceSuccess>(DeviceActionTypes.AddDeviceSuccess),
        map(action => action.payload),        
        tap(payload => {
            this.snackBar.open(payload, 'OK', { duration: 3000 });
        }),
    );
    
    @Effect()
    submitDevice$ = this.actions$.pipe(
        ofType<SubmitDevice>(DeviceActionTypes.SubmitDevice),
        map(action => action.payload),
        switchMap(payload => {
            return this.deviceService.submit(payload.id, payload.serialNumber, payload.name);
        }),
        switchMap((data: any) => {
            return [new SubmitDeviceSuccess(data.message), new GetDevices()];
        })
    );

    @Effect({ dispatch: false })
    submitDeviceSuccess$ = this.actions$.pipe(
        ofType<SubmitDeviceSuccess>(DeviceActionTypes.SubmitDeviceSuccess),
        map(action => action.payload),        
        tap(payload => {
            this.snackBar.open(payload, 'OK', { duration: 3000 });
        }),
    );

    @Effect()
    deleteDevice$ = this.actions$.pipe(
        ofType<DeleteDevice>(DeviceActionTypes.DeleteDevice),
        map(action => action.payload),
        switchMap((id) => {
            return this.deviceService.deleteDevice(id);
        }),
        tap(data => {
            this.snackBar.open(data.message, 'OK', { duration: 3000 });
        }),
        switchMap(() => {
            return of(new GetDevices());
        })
    );

    constructor(
        private deviceService: DeviceService,
        private actions$: Actions,
        private snackBar: MatSnackBar,
    ) { }
}
