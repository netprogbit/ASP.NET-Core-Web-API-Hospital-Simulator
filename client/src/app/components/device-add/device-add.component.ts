import { Component, OnInit, Inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAppState } from 'src/app/core/store/app/app.state';
import { AddDevice } from 'src/app/core/store/device/device.actions';

@Component({
  selector: 'app-device-add',
  templateUrl: './device-add.component.html',
  styleUrls: ['./device-add.component.css']
})
export class DeviceAddComponent implements OnInit {

  public editForm: FormGroup;

  constructor(private store: Store<IAppState>, private formBuilder: FormBuilder, 
    public dialogRef: MatDialogRef<DeviceAddComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {

    this.editForm = this.formBuilder.group({
      id: ['', Validators.required],
      serialNumber: ['', Validators.required],
      name: ['', Validators.required],
    });

    this.editForm.get('id').setValue(0);    
  }

  // Editing form controls getter
  get efc() {
    return this.editForm.controls;
  }  

  public onSubmit(): void {
    this.store.dispatch(new AddDevice({
      id: this.editForm.value.id,
      serialNumber: this.editForm.value.serialNumber,
      name: this.editForm.value.name,
    }));

    this.onCancel();
  }

  public onCancel(): void {
    this.dialogRef.close();
  }  

}
