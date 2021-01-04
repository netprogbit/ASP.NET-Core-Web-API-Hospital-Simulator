import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SummaryListRoutingModule } from './summary-list-routing.module';
import { SummaryListComponent } from './summary-list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from 'src/app/shared/material/material.module';


@NgModule({
  declarations: [SummaryListComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SummaryListRoutingModule,
    MaterialModule
  ]
})
export class SummaryListModule { }
