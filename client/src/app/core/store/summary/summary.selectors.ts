import { IAppState } from '../app/app.state';
import { ISummaryState } from './summary.state';
import { createSelector } from '@ngrx/store';

const summary = (state: IAppState) => state.summary;
export const summaries = createSelector(summary, (state: ISummaryState) => state.summaries);