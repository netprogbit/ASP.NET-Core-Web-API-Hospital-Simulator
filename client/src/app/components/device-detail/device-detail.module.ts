import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceDetailRoutingModule } from './device-detail-routing.module';
import { DeviceDetailComponent } from './device-detail.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/material/material.module';


@NgModule({
  declarations: [DeviceDetailComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DeviceDetailRoutingModule,
    MaterialModule
  ]
})
export class DeviceDetailModule { }
