import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { Store, select } from '@ngrx/store';
import { AppState } from '../../app.state';
import * as Auth from '../actions/auth.actions';
import { Observable } from 'rxjs';
import * as fromAuth from '../reducers/auth.reducer';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  userForm: FormGroup;
  user: User = new User();
  errorMessage: string;

  constructor(private router: Router, private store: Store<AppState>) {
  }

  ngOnInit() {
    this.userForm = new FormGroup({
      'username': new FormControl(this.user.username, [
        Validators.required
      ]),
      'password': new FormControl(this.user.password, [
        Validators.required
      ])
    });
  }

  onSubmit(): void {
    this.store.dispatch(new Auth.Login(this.userForm.value));
  }
}
