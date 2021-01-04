import { initialDeviceState, IDeviceState } from './device.state';
import { DeviceActions, DeviceActionTypes } from './device.actions';

export const deviceReducer = (state = initialDeviceState, action: DeviceActions): IDeviceState => {
    switch (action.type) {
        case DeviceActionTypes.GetDevicesSuccess: {
            return {
                ...state,
                devices: action.payload,
            };
        }                                       
        default:
            return state;
    }
};