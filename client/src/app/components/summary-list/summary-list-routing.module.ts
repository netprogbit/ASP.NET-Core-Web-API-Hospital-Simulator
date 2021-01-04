import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SummaryListComponent } from './summary-list.component';

const routes: Routes = [
  {
    path: '',
    component: SummaryListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SummaryListRoutingModule { }
