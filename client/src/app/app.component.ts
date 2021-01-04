import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>'
})
export class AppComponent implements OnInit {

  title = 'client';

  constructor(private authService: AuthService) { }

  ngOnInit() {
    const patientId: number = parseInt(localStorage.getItem('patientId'), 10);
    const role: string = localStorage.getItem('role');
    const token: string = localStorage.getItem('token');

    if (patientId && role && token) {
      this.authService.setAuth(patientId, role, token);
    }
  }
}
