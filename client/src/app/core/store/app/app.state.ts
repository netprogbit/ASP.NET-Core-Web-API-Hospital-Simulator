import { RouterReducerState } from "@ngrx/router-store";
import { IAuthState, initialAuthState } from "../auth/auth.state";
import { IDeviceState,initialDeviceState } from "../device/device.state";
import { ISummaryState, initialSummaryState } from "../summary/summary.state";

export interface IAppState {
    router?: RouterReducerState,
    auth: IAuthState,    
    device: IDeviceState,
    sumary: ISummaryState    
}

export const initialAppState: IAppState = {
    auth: initialAuthState,
    device: initialDeviceState,
    sumary: initialSummaryState,    
};

export function getInitialState(): IAppState {
    return initialAppState;
}