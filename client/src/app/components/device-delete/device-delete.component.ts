import { Component, OnInit, Inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAppState } from 'src/app/core/store/app/app.state';
import { DeleteDevice } from 'src/app/core/store/device/device.actions';


@Component({
  selector: 'app-device-delete',
  templateUrl: './device-delete.component.html',
  styleUrls: ['./device-delete.component.css']
})
export class DeviceDeleteComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<DeviceDeleteComponent>, private store: Store<IAppState>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {

  }

  onDelete(): void {
    this.store.dispatch(new DeleteDevice(this.data.id));
    this.onCancel();    
  }

  onCancel(): void {
    this.dialogRef.close();
  }  

}
