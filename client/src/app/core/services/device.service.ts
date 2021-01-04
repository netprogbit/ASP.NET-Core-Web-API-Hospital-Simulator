import { mergeMap } from 'rxjs/operators';
import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { IDevice } from '../models/device.interface';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private httpClient: HttpClient) { }

  public getDevices(): Observable<IDevice[]> {
    const devicesUrl = `${environment.apiUrl + environment.apiDevices}`;
    return this.httpClient.get<IDevice[]>(devicesUrl).pipe(
      mergeMap(data => {
        return of(data);
      })
    );
  }

  public add(id: number, serialNumber: string, name: string): Observable<any> {
    const deviceSubmitUrl = `${environment.apiUrl + environment.apiDevice}`;
    const body = { id, serialNumber, name };
    return this.httpClient.post(deviceSubmitUrl, body);
  }

  public submit(id: number, serialNumber: string, name: string): Observable<any> {
    const deviceSubmitUrl = `${environment.apiUrl + environment.apiDevice}`;
    const body = { id, serialNumber, name };
    return this.httpClient.put(deviceSubmitUrl, body);
  }

  public deleteDevice(id: number): Observable<any> {
    const deviceDeleteUrl = `${environment.apiUrl + environment.apiDevice + id}`;
    return this.httpClient.delete(deviceDeleteUrl);
  }
}
