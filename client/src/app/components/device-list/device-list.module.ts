import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeviceListRoutingModule } from './device-list-routing.module';
import { DeviceListComponent } from './device-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/material/material.module';


@NgModule({
  declarations: [DeviceListComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DeviceListRoutingModule,
    MaterialModule
  ]
})
export class DeviceListModule { }
