import { Routes, RouterModule } from '@angular/router';
import { ConfirmationComponent } from './confirmation.component';
import { ModuleWithProviders } from '@angular/core';

const confirmationRoutes: Routes = [
  {
    path: '',
    component: ConfirmationComponent
  }
];

export const ConfirmationRouting: ModuleWithProviders = RouterModule.forChild(confirmationRoutes);
