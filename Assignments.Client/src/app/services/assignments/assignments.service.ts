import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../../../config';
import { Observable } from 'rxjs';
import {
  AddAssignment,
  Assignment,
  AssignmentRequest,
} from '../../interfaces/assignments';
import { Pagination } from '../../interfaces/pagination';
import { RequestStr, RequestUpdateSingle } from '../../interfaces/requstStr';

@Injectable({
  providedIn: 'root',
})
export class AssignmentsService {
  private baseUrl: string = AppConfig.baseUrl;
  constructor(private http: HttpClient) {}

  assignmentEnd():Observable<any>{
    return this.http.get<any>(`${this.baseUrl}/Assignment/UpdateEndAssignments`);
  }

  getAssignmentTypes(): Observable<any> {
    return this.http.get(`${this.baseUrl}/Assignment/GetAssignmentListType`);
  }

  getAssignmentsByDate(request: AssignmentRequest, archive:boolean): Observable<any> {
    const { first, rows, sortField, sortOrder } = request;

    let getAssignmentUrl = '';
    if (archive === true) {
      getAssignmentUrl = 'GetAssignmentsByDateWithArchive';
    }

    debugger;
    const page = first / rows + 1;
    const params: Pagination = {
      page,
      pageSize: rows,
      sort: sortField,
      order: sortOrder === 1 ? true : false,
    };
    //GetAssignmentsByDateWithArchive
    return this.http.post<any>(
      `${this.baseUrl}/Assignment/GetAssignmentsByDate${
        archive ? 'WithArchive' : ''
      }`,
      params
    );
  }

  completeAssignments(assignmentsComplete: string): Observable<any> {
    const request: RequestStr = {
      updateArr: assignmentsComplete,
    };
    return this.http.patch(
      `${this.baseUrl}/Assignment/UpdateAssignmentComplete`,
      request
    );
  }

  updateArchive(req: RequestUpdateSingle): Observable<any> {
    return this.http.patch(
      `${this.baseUrl}/Assignment/UpdateAssignmentArchive`,
      req
    );
  }

  updatePeriodic(req: RequestUpdateSingle): Observable<any> {
    return this.http.patch(
      `${this.baseUrl}/Assignment/UpdateAssignmentPeriodic`,
      req
    );
  }

  addAssignment(postAssignment: AddAssignment) {
    return this.http.post(
      `${this.baseUrl}/Assignment/CreateAssignment`,
      postAssignment
    );
  }

  deleteAssignment(id: Assignment['assignmentId']) {
    return this.http.delete(`${this.baseUrl}/Assignment/${id}`);
  }

  updateAssignment(postAssignment: Assignment) {
    return this.http.patch(
      `${this.baseUrl}/assignment/${postAssignment.assignmentId}`,
      postAssignment
    );
  }
}
