import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IMeasurement } from 'src/app/core/models/measurement.interface';
import { SignalrService } from 'src/app/core/services/signalr.service';
import { IAppState } from 'src/app/core/store/app/app.state';
import { ChangeMeasurement } from 'src/app/core/store/device/device.actions';
import { measurement } from 'src/app/core/store/device/device.selectors';

@Component({
  selector: 'app-device-detail',
  templateUrl: './device-detail.component.html',
  styleUrls: ['./device-detail.component.css'],
  providers: [SignalrService]
})
export class DeviceDetailComponent implements OnInit {

  public measurement$: Observable<IMeasurement> = this.store.pipe(select(measurement));

  constructor(private store: Store<IAppState>, private route: ActivatedRoute, private signalRService: SignalrService) { }

  ngOnInit(): void {
    this.store.dispatch(new ChangeMeasurement(null)); // Clear measurement   
    const deviceId = +this.route.snapshot.paramMap.get('id'); // Get deviceId from route            
    this.signalRService.startConnection(); // SignarR connection
    this.signalRService.addTransferStatusDataListener(deviceId); // Add SignalR listener
  }

}
