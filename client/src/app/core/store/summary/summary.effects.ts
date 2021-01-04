import { Effect, ofType, Actions } from '@ngrx/effects';
import { Injectable } from '@angular/core';
import {
    GetSummaries,
    GetSummariesSuccess,        
    SummaryActionTypes
} from './summary.actions';
import { of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { ISummary } from '../../models/summary.interface';
import { SummaryService } from '../../services/summary.service';

@Injectable()
export class SummaryEffects {

    @Effect()
    getSumaries$ = this.actions$.pipe(
        ofType<GetSummaries>(SummaryActionTypes.GetSummaries),        
        switchMap(() => {
            return this.deviceService.getDevices();
        }),
        switchMap((devices: ISummary[]) => {
            return of(new GetSummariesSuccess(devices));
        })
    );
          
    constructor(
        private deviceService: SummaryService,
        private actions$: Actions,
    ) { }
}
