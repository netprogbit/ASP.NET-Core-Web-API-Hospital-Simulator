import { Action } from '@ngrx/store';
import { IDevice } from '../../models/device.interface';
import { IMeasurement } from '../../models/measurement.interface';

export enum DeviceActionTypes {
    GetDevices = '[Device] Get Devices',
    GetDevicesSuccess = '[Device] Get Devices Success',
    AddDevice = '[Device] Add Device',
    AddDeviceSuccess = '[Device] Add Device Success',        
    SubmitDevice = '[Device] Submit Device',
    SubmitDeviceSuccess = '[Device] Submit Device Success',           
    DeleteDevice = '[Device] Delete Device',
    ChangeMeasurement = '[Device] Change Measurement',
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

export class ChangeMeasurement implements Action {
    public readonly type = DeviceActionTypes.ChangeMeasurement;
    constructor(public payload: IMeasurement) { }
}

export type DeviceActions =
    | GetDevices
    | GetDevicesSuccess
    | AddDevice
    | AddDeviceSuccess   
    | SubmitDevice
    | SubmitDeviceSuccess
    | DeleteDevice
    | ChangeMeasurement