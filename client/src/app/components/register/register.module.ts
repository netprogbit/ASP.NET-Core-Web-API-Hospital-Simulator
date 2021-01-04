import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterRoutingModule } from './register-routing.module';
import { RegisterComponent } from './register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/material/material.module';


@NgModule({
  declarations: [RegisterComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RegisterRoutingModule,
    MaterialModule
  ]
})
export class RegisterModule { }
