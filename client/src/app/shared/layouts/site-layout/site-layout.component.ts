import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { NgxPermissionsService } from 'ngx-permissions';

@Component({
  selector: 'app-site-layout',
  templateUrl: './site-layout.component.html',
  styleUrls: ['./site-layout.component.css']
})
export class SiteLayoutComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router, private permissionsService: NgxPermissionsService) { }

  ngOnInit(): void {
    const role = this.authService.getRole();           
    this.permissionsService.loadPermissions([ role ]);
  }

  logout(event: Event) {
    event.preventDefault();
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}
