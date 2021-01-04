import { ISummary } from "../../models/summary.interface";

export interface ISummaryState {    
    summaries: ISummary[],        
}

export const initialSummaryState: ISummaryState = {    
    summaries: [],        
};