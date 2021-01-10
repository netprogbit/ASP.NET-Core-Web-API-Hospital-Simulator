import { mergeMap } from 'rxjs/operators';
import { environment } from './../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { Injectable } from '@angular/core';
import { IDevice } from '../models/device.interface';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  constructor(private httpClient: HttpClient, private authService: AuthService) { }

  public getDevices(): Observable<IDevice[]> {
    const patientId = this.authService.getPatientId(); 
    const devicesUrl = `${environment.apiUrl + environment.apiDevices + patientId}`;       
    return this.httpClient.get<IDevice[]>(devicesUrl).pipe(
      mergeMap(data => {
        return of(data);
      })
    );
  }

  public add(id: number, serialNumber: string, name: string): Observable<any> {
    const deviceSubmitUrl = `${environment.apiUrl + environment.apiDevice}`;
    const patientId = this.authService.getPatientId();
    const body = { id, patientId, serialNumber, name };
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
