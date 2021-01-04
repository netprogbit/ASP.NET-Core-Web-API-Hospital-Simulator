import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { IAppState } from 'src/app/core/store/app/app.state';
import { Login } from 'src/app/core/store/auth/auth.actions';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public loginForm: FormGroup;  

  constructor(private store: Store<IAppState>, private formBuilder: FormBuilder) { }

  ngOnInit() {
    
    this.loginForm = this.formBuilder.group({                        
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });    
  }

  // Login form controls getter
  get efc() {
    return this.loginForm.controls;
  }

  onSubmit() {
      this.store.dispatch(new Login(this.loginForm.value));          
  }
  
}
