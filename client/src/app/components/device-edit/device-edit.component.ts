import { Component, OnInit, Inject } from '@angular/core';
import { Store } from '@ngrx/store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { IAppState } from 'src/app/core/store/app/app.state';
import { SubmitDevice } from 'src/app/core/store/device/device.actions';

@Component({
  selector: 'app-device-edit',
  templateUrl: './device-edit.component.html',
  styleUrls: ['./device-edit.component.css']
})
export class DeviceEditComponent implements OnInit {

  public editForm: FormGroup;

  constructor(private store: Store<IAppState>, private formBuilder: FormBuilder, 
    public dialogRef: MatDialogRef<DeviceEditComponent>, @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {

    this.editForm = this.formBuilder.group({
      id: ['', Validators.required],
      serialNumber: ['', Validators.required],
      name: ['', Validators.required],
    });

    this.editForm.get('id').setValue(this.data.id);
    this.editForm.get('serialNumber').setValue(this.data.serialNumber);
    this.editForm.get('name').setValue(this.data.name);   
  }

  // Editing form controls getter
  get efc() {
    return this.editForm.controls;
  }  

  public onSubmit(): void {
    this.store.dispatch(new SubmitDevice({
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
