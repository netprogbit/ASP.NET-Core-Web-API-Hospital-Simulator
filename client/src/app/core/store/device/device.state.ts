import { IDevice } from "../../models/device.interface";
import { IMeasurement } from "../../models/measurement.interface";

export interface IDeviceState {    
    devices: IDevice[],
    measurement: IMeasurement        
}

export const initialDeviceState: IDeviceState = {    
    devices: [],
    measurement: null        
};