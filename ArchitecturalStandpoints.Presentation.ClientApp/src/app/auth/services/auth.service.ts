import { Injectable } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor() { }

  logIn(user: User): Observable<any> {
    /**
     * Simulate a failed login to display the error
     * message for the login form.
     */
    if (user.username !== 'test' || user.password !== 'test') {
      return throwError('Invalid user ID or password');
    }

    return of('User');
  }

  logOut() {
    return of(true);
  }
}