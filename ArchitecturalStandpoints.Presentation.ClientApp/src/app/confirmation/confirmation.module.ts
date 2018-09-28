import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmationComponent } from './confirmation.component';
import { ConfirmationRouting } from './component.routing';

@NgModule({
  imports: [
    CommonModule,
    ConfirmationRouting
  ],
  declarations: [ConfirmationComponent]
})
export class ConfirmationModule { }
