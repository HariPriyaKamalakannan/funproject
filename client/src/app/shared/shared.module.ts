import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './pagination-header/pagination-header.component';
import { PagerComponent } from './pager/pager.component';
import { OrderTotalsComponent } from './order-totals/order-totals.component';

import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextInputComponent } from './components/text-input/text-input.component';
import { StepperComponent } from './components/stepper/stepper.component';
import {CdkStepperModule} from '@angular/cdk/stepper';
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,BsDropdownModule,FormsModule,
    CdkStepperModule,RouterModule
  ],
  exports:[
    PaginationModule,
    PaginationHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    ReactiveFormsModule,BsDropdownModule,
    TextInputComponent,
    StepperComponent, CdkStepperModule,BasketSummaryComponent
  ]
})
export class SharedModule { }
