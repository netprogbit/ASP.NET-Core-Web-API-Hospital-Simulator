import { routerReducer } from '@ngrx/router-store';
import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from './app.state';
import { authReducer } from '../auth/auth.reducer';
import { deviceReducer } from '../device/device.reducer';
import { summaryReducer } from '../summary/summary.reducer';

export const appReducers: ActionReducerMap<IAppState> = {
    router: routerReducer,
    auth: authReducer,    
    device: deviceReducer,
    summary: summaryReducer    
};
