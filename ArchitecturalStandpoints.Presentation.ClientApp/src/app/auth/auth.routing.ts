import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login.component';
import { ModuleWithProviders } from '@angular/core';

const authRoutes: Routes = [
  {
    path: '',
    component: LoginComponent
  }
];

export const AuthRouting: ModuleWithProviders = RouterModule.forChild(authRoutes);
