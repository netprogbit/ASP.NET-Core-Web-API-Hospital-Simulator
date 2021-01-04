import { IAppState } from '../app/app.state';
import { ISummaryState } from './summary.state';
import { createSelector } from '@ngrx/store';

const device = (state: IAppState) => state.device;
export const summaries = createSelector(device, (state: ISummaryState) => state.summaries);