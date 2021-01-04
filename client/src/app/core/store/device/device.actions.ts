import { Action } from '@ngrx/store';
import { IDevice } from '../../models/device.interface';

export enum DeviceActionTypes {
    GetDevices = '[User] Get Devices',
    GetDevicesSuccess = '[User] Get Devices Success',
    AddDevice = '[User] Add Device',
    AddDeviceSuccess = '[User] Add Device Success',        
    SubmitDevice = '[User] Submit Device',
    SubmitDeviceSuccess = '[User] Submit Device Success',           
    DeleteDevice = '[User] Delete Device',
}

export class GetDevices implements Action {
    public readonly type = DeviceActionTypes.GetDevices;    
}

export class GetDevicesSuccess implements Action {
    public readonly type = DeviceActionTypes.GetDevicesSuccess;
    constructor(public payload: IDevice[]) { }
}

export class AddDevice implements Action {
    public readonly type = DeviceActionTypes.AddDevice;
    constructor(public payload: any) { }
}

export class AddDeviceSuccess implements Action {
    public readonly type = DeviceActionTypes.AddDeviceSuccess;
    constructor(public payload: string) { }
}

export class SubmitDevice implements Action {
    public readonly type = DeviceActionTypes.SubmitDevice;
    constructor(public payload: any) { }
}

export class SubmitDeviceSuccess implements Action {
    public readonly type = DeviceActionTypes.SubmitDeviceSuccess;
    constructor(public payload: string) { }
}

export class DeleteDevice implements Action {
    public readonly type = DeviceActionTypes.DeleteDevice;
    constructor(public payload: number) { }
}

export type DeviceActions =
    | GetDevices
    | GetDevicesSuccess
    | AddDevice
    | AddDeviceSuccess   
    | SubmitDevice
    | SubmitDeviceSuccess
    | DeleteDevice