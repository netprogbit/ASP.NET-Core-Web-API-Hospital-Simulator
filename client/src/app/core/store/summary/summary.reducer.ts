import { initialSummaryState, ISummaryState } from './summary.state';
import { SummaryActions, SummaryActionTypes } from './summary.actions';

export const summaryReducer = (state = initialSummaryState, action: SummaryActions): ISummaryState => {
    switch (action.type) {
        case SummaryActionTypes.GetSummariesSuccess: {
            return {
                ...state,
                summaries: action.payload,
            };
        }                                       
        default:
            return state;
    }
};