import * as fromAuth from './auth/reducers/auth.reducer';
  
export interface AppState {
    auth: fromAuth.State;
}