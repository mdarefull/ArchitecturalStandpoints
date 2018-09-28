import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardComponent } from './components/dashboard.component';
import { DashboardRouting } from './dashboard.routing';
import { ClientCardComponent } from './components/client-card.component';

@NgModule({
  imports: [
    CommonModule,
    DashboardRouting
  ],
  declarations: [
    DashboardComponent,
    ClientCardComponent
  ]
})
export class DashboardModule { }
