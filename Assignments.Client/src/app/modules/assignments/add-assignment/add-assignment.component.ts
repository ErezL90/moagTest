import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { MessageService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AssignmentsService } from '../../../services/assignments/assignments.service';
import { AddAssignment } from '../../../interfaces/assignments';
import { DatePipe } from '@angular/common';
import { AssignmentTypes } from '../../../interfaces/assignmentTypes';

@Component({
  selector: 'app-add-assignment',
  templateUrl: './add-assignment.component.html',
  styleUrl: './add-assignment.component.css',
})
export class AddAssignmentComponent implements OnInit {
  data: any;
  newItem!: AddAssignment;

  form!: FormGroup;

  typeOptions: AssignmentTypes[] = [];

  constructor(
    private fb: FormBuilder,
    private messageService: MessageService,
    private assignmentsService: AssignmentsService,
    private ref: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.data = this.config.data;

    this.form = this.fb.group({
      assignmentName: ['', [Validators.required]],
      description: ['', [Validators.required]],
      assignmentTypeId: ['', [Validators.required]],
      startDate: [new Date(), [Validators.required]],
      endDate: [new Date(), [Validators.required]],
      isPeriodic: [false, [Validators.required]],
    });

    this.assignmentsService.getAssignmentTypes().subscribe(
      (data) => {
        for (let i = 0; i < data.length; i++) {
          const obj: AssignmentTypes = {
            assignmentTypeName: data[i].assignmentTypeName,
            assignmentTypeId: data[i].assignmentTypeId,
          };
          this.typeOptions.push(obj);
        }
        console.log(this.typeOptions);
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'ERROR!',
        });
      }
    );
  }

  closeDialog() {
    this.ref.close();
  }

  onSubmit() {
    if (this.form.valid) {
      console.log(this.form.value);
      this.newItem = { ...this.form.value };
      this.newItem.startDate = this.datePipe
        .transform(this.newItem.startDate, 'yyyy-dd-MMTHH:mm:ss')!
        .toString();
      this.newItem.endDate = this.datePipe
        .transform(this.newItem.endDate, 'yyyy-dd-MMTHH:mm:ss')!
        .toString();

      console.log(this.newItem);

      this.assignmentsService.addAssignment(this.newItem).subscribe(
        (response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Assignment added successfully',
          });
          this.ref.close();
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: 'Something went wrong with server...',
          });
        }
      );
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Please fill in all required fields',
      });
    }
  }
}
