import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './components/dashboard.component';
import { ModuleWithProviders } from '@angular/core';

const dashboardRoutes: Routes = [
  {
    path: '',
    component: DashboardComponent
  }
];

export const DashboardRouting: ModuleWithProviders = RouterModule.forChild(dashboardRoutes);
