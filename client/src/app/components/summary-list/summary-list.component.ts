import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { ISummary } from 'src/app/core/models/summary.interface';
import { IAppState } from 'src/app/core/store/app/app.state';
import { GetSummaries } from 'src/app/core/store/summary/summary.actions';
import { summaries } from 'src/app/core/store/summary/summary.selectors';

@Component({
  selector: 'app-summary-list',
  templateUrl: './summary-list.component.html',
  styleUrls: ['./summary-list.component.css']
})
export class SummaryListComponent implements OnInit {

  public displayedColumns: string[] = ['patientId', 'patientName', 'deviceId', 'deviceSerialNumber', 'deviceName', 'currentHR', 'currentRR', 'avgHR', 'avgRR', 'actions'];    
  public summaries$: Observable<ISummary[]> = this.store.pipe(select(summaries));

  constructor(private store: Store<IAppState>) { }

  ngOnInit() {  
    this.store.dispatch(new GetSummaries()); // Getting summaries    
  } 

}
