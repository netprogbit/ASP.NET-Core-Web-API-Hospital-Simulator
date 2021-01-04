import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SiteLayoutComponent } from './shared/layouts/site-layout/site-layout.component';
import { AuthLayoutComponent } from './shared/layouts/auth-layout/auth-layout.component';
import { AuthGuard } from './core/guards/auth-guard';
import { AdminGuard } from './core/guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    component: SiteLayoutComponent,
    canActivate: [AuthGuard],    
    data: { pageTitle: "Site"},
    children: [
      {
        path: '', redirectTo: '/devices', pathMatch: 'full'
      },
      { 
        path: 'devices', 
        loadChildren: () => import('./components/device-list/device-list.module').then(m => m.DeviceListModule)
      },
      { 
        path: 'summaries',
        canActivate: [AdminGuard], 
        loadChildren: () => import('./components/device-list/device-list.module').then(m => m.DeviceListModule)
      },      
    ]
  }, 
  {
    path: '',
    component: AuthLayoutComponent,
    data: { pageTitle: "Auth"},
    children: [
      {
        path: '', redirectTo: '/login', pathMatch: 'full'
      },
      { 
        path: 'register', 
        loadChildren: () => import('./components/register/register.module').then(m => m.RegisterModule)
      },
      {
        path: 'login',
        loadChildren: () => import('./components/login/login.module').then(m => m.LoginModule)
      }
    ]
  },     
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
