import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Store, select } from '@ngrx/store';
import { Observable } from 'rxjs';
import { IDevice } from 'src/app/core/models/device.interface';
import { IAppState } from 'src/app/core/store/app/app.state';
import { GetDevices } from 'src/app/core/store/device/device.actions';
import { devices } from 'src/app/core/store/device/device.selectors';
import { DeviceAddComponent } from '../device-add/device-add.component';
import { DeviceDeleteComponent } from '../device-delete/device-delete.component';
import { DeviceEditComponent } from '../device-edit/device-edit.component';

@Component({
  selector: 'app-device-list',
  templateUrl: './device-list.component.html',
  styleUrls: ['./device-list.component.css']
})
export class DeviceListComponent implements OnInit {

  public displayedColumns: string[] = ['id', 'serialNumber', 'name', 'actions'];    
  public devices$: Observable<IDevice[]> = this.store.pipe(select(devices));

  constructor(public dialog: MatDialog, private store: Store<IAppState>) { }

  ngOnInit() {  
    this.store.dispatch(new GetDevices()); // Getting devices    
  }    
  
  public add(): void {

    this.dialog.open(DeviceAddComponent, {
      data: { id: 0 }
    });
  }

  public edit(id: number, serialNumber: string, name: string): void {

    this.dialog.open(DeviceEditComponent, {
      data: { id, serialNumber, name }
    });
  }

  public delete(id: number, serialNumber: string, name: string): void {

    this.dialog.open(DeviceDeleteComponent, {
      data: { id, serialNumber, name }
    });
  } 

}
