import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { AuthLayoutComponent } from './shared';
import { AppComponent } from './app.component';

export const routes: Routes = [
  {
    path: 'login',
    component: AppComponent,
    children: [
      {
        path: '',
        loadChildren: './auth/auth.module#AuthModule'
      }
    ]
  },
  {
    path: 'dashboard',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: './dashboard/dashboard.module#DashboardModule'
      },
      {
        path: 'confirmation',
        loadChildren: './confirmation/confirmation.module#ConfirmationModule'
      }
    ]
  },
  {
    path: '**', redirectTo: 'login', pathMatch: 'full'
  }
];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(routes);
