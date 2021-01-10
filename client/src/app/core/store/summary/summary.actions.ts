import { Action } from '@ngrx/store';
import { ISummary } from '../../models/summary.interface';

export enum SummaryActionTypes {
    GetSummaries = '[Summary] Get Summaries',
    GetSummariesSuccess = '[Summary] Get Summaries Success',    
}

export class GetSummaries implements Action {
    public readonly type = SummaryActionTypes.GetSummaries;    
}

export class GetSummariesSuccess implements Action {
    public readonly type = SummaryActionTypes.GetSummariesSuccess;
    constructor(public payload: ISummary[]) { }
}

export type SummaryActions =
    | GetSummaries
    | GetSummariesSuccess    