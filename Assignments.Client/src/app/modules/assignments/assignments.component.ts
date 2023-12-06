import { Component, OnInit } from '@angular/core';
import { AssignmentsService } from '../../services/assignments/assignments.service';
import { Assignment, AssignmentRequest } from '../../interfaces/assignments';
import { TableLazyLoadEvent } from 'primeng/table';
import { DialogService } from 'primeng/dynamicdialog';
import { AddAssignmentComponent } from './add-assignment/add-assignment.component';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { RequestUpdateSingle } from '../../interfaces/requstStr';
import { AssignmentTypes } from '../../interfaces/assignmentTypes';

@Component({
  selector: 'app-assinments',
  templateUrl: './assignments.component.html',
  styleUrls: ['./assignments.component.css'],
})
export class AssinmentsComponent implements OnInit {
  yesAns = 'YES';
  noAns = 'NO';
  archiveProp:string = 'Archive'
  globalFilter = '';
  totalRecords: number = 0;
  totalPages: number = 0;
  assignmentsRes: Assignment[] = [];
  assignmentsTypes: AssignmentTypes[] = [];

  shArchive:boolean = false;

  request: AssignmentRequest = {
    first: 0,
    rows: 3,
    sortField: '',
    sortOrder: 1,
    filter: {
      AssignmentName: '',
    },
  };

  constructor(
    private dialogService: DialogService,
    private assignmentsService: AssignmentsService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    debugger;
    this.assignmentsService.assignmentEnd().subscribe(
      (data)=>{
        debugger;
      },
      (error)=>{

      }
    );

    this.assignmentsService.getAssignmentTypes().subscribe(
      (data) => {
        for (let i = 0; i < data.length; i++) {
          const obj: AssignmentTypes = {
            assignmentTypeName: data[i].assignmentTypeName,
            assignmentTypeId: data[i].assignmentTypeId,
          };
          this.assignmentsTypes.push(obj);
        }
        console.log(this.assignmentsTypes);
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

  errorMsg(error: any) {
    let errorMsg = '';
    if (error.status === 401) {
      errorMsg = 'User Unauthorized. Please login again!';
      localStorage.clear();
      this.router.navigate(['login']);
    } else if (error.status === 403) {
      errorMsg = 'User Forbidden';
    } else {
      errorMsg = 'Something error...';
    }

    this.messageService.add({
      severity: 'error',
      summary: 'Error',
      detail: errorMsg,
    });
  }

  getAssignmentByDate() {
    this.assignmentsService.getAssignmentsByDate(this.request, this.shArchive).subscribe(
      (data) => {
        this.assignmentsRes = data.assignmentRes;
        this.totalRecords = data.totalRecords;
        this.totalPages = data.totalPages;
      },
      (error) => {
        this.errorMsg(error);
      }
    );
  }

  toggleArchive() {
    this.shArchive = !this.shArchive;
    this.archiveProp = (this.archiveProp !== 'Archive') ? 'Archive' : 'Without Archive';
    this.getAssignmentByDate();
  }

  loadAssignments($event: TableLazyLoadEvent) {
    console.log($event);
    this.request.sortField = $event.sortField || '';
    this.request.sortOrder = $event.sortOrder || 1;
    this.request.first = $event.first || 0;
    this.getAssignmentByDate();
  }

  addAssignment() {
    const ref = this.dialogService.open(AddAssignmentComponent, {
      header: 'Dialog Title',
      data: { message: 'Hello from the parent component!' },
    });

    ref.onClose.subscribe(() => {
      console.log('Dialog closed');
    });
    this.getAssignmentByDate();
  }

  addtolocalComplete($event: any, assignment: Assignment) {
    const assN = 'assignment' + assignment.assignmentId?.toString();
    const item = localStorage.getItem(assN);
    if (!item) {
      if ($event.checked) {
        localStorage.setItem(assN, $event.checked);
      }
    }

    if (item && !$event.checked) {
      localStorage.removeItem(assN);
    }
  }

  getKeysWithStr(str: string) {
    const keys = Object.keys(localStorage);
    let ret: string = '';
    const filteredKeys = keys.filter((key) => {
      return key.startsWith(str);
    });

    filteredKeys.forEach((e) => {
      const m = e.replace('assignment', '');
      localStorage.removeItem(e);
      ret += m + '*';
    });

    return ret;
  }

  completeAssignment() {
    const arr = this.getKeysWithStr('assignment');

    this.assignmentsService.completeAssignments(arr).subscribe(
      (data) => {
        let msg: string = '';
        if (data > 0) {
          msg = `${data} rows affected`;
          this.getAssignmentByDate();
        } else {
          msg = 'no rows affected';
        }

        this.messageService.add({
          severity: 'info',
          summary: 'Info',
          detail: msg,
        });
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

  archiveAssignment(
    updateArchive: Assignment['isArchive'],
    idAssignment: Assignment['assignmentId']
  ) {
    const req: RequestUpdateSingle = {
      id: idAssignment!,
      update: !updateArchive,
    };

    this.assignmentsService.updateArchive(req).subscribe(
      (data) => {
        this.messageService.add({
          severity: 'info',
          summary: 'Info',
          detail: `Assignment ${data !== 1 ? 'not' : ''} archived`,
        });
        this.getAssignmentByDate();
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: `Error archived`,
        });
      }
    );
  }

  periodicAssignment(
    updatePeriodic: Assignment['isPeriodic'],
    idAssignment: Assignment['assignmentId']
  ) {
    const req: RequestUpdateSingle = {
      id: idAssignment!,
      update: !updatePeriodic,
    };

    console.log('archiveAssignment');
    this.assignmentsService.updatePeriodic(req).subscribe(
      (data) => {
        this.messageService.add({
          severity: 'info',
          summary: 'Info',
          detail: `Assignment ${data !== 1 ? 'not' : ''} periodic`,
        });
        this.getAssignmentByDate();
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: `Error periodic`,
        });
      }
    );
  }
  deleteAssignment(id: Assignment['assignmentId']) {
    this.assignmentsService.deleteAssignment(id).subscribe(
      (data) => {
        if (data === 1) {
          this.messageService.add({
            severity: 'info',
            summary: 'Info',
            detail: `Assignment deleted`,
          });
          this.getAssignmentByDate();
        }
      },
      (error) => {
        this.errorMsg(error);
      }
    );
  }
}
