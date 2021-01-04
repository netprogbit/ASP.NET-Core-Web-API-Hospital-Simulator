import { IDevice } from "../../models/device.interface";

export interface IDeviceState {    
    devices: IDevice[],        
}

export const initialDeviceState: IDeviceState = {    
    devices: [],        
};