import { mergeMap } from 'rxjs/operators';
import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { ISummary } from '../models/summary.interface';

@Injectable({
  providedIn: 'root'
})
export class SummaryService {

  constructor(private httpClient: HttpClient) { }

  public getSummaries(): Observable<ISummary[]> {
    const summariesUrl = `${environment.apiUrl + environment.apiSummaries}`;
    return this.httpClient.get<ISummary[]>(summariesUrl).pipe(
      mergeMap(data => {
        return of(data);
      })
    );
  }  
}
