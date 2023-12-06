import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AssinmentsComponent } from './assignments.component';

import {
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { TagModule } from 'primeng/tag';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { AddAssignmentComponent } from './add-assignment/add-assignment.component';
import { DialogService } from 'primeng/dynamicdialog';

import { ReactiveFormsModule } from '@angular/forms';
import { loggerInterceptor } from '../../interceptor/logger.interceptor';
import { errorInterceptor } from '../../interceptor/error.interceptor';
import { AssignmentTypePipe } from '../../pipes/assignmenttype.pipe';

@NgModule({
  declarations: [
    AssinmentsComponent,
    AddAssignmentComponent,
    AssignmentTypePipe,
  ],
  imports: [
    CommonModule,
    DialogModule,
    ReactiveFormsModule,
    HttpClientModule,
    TableModule,
    InputTextModule,
    FormsModule,
    TagModule,
    CheckboxModule,
    ButtonModule,
    CalendarModule,
    DropdownModule,
  ],
  exports: [AssinmentsComponent, AddAssignmentComponent],
  providers: [
    DialogService,
    provideHttpClient(withInterceptors([loggerInterceptor, errorInterceptor])),
  ],
})
export class AssinmentsModule {}
//{provide:HTTP_INTERCEPTORS,useClass:authinterceptorInterceptor,multi:true}
