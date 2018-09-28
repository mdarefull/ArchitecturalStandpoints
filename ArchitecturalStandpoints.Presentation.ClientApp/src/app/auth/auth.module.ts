import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login.component';
import { AuthRouting } from './auth.routing';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    AuthRouting,
    ReactiveFormsModule
  ],
  declarations: [LoginComponent]
})
export class AuthModule { }
