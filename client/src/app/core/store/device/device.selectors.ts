import { IAppState } from '../app/app.state';
import { IDeviceState } from './device.state';
import { createSelector } from '@ngrx/store';

const device = (state: IAppState) => state.device;
export const devices = createSelector(device, (state: IDeviceState) => state.devices);