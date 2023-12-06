export interface Assignment {
  assignmentId?: number;
  assignmentName: string;
  description: string;
  startDate: string;
  endDate: string;
  isPeriodic: boolean;
  isCompleted: boolean;
  isArchive: boolean;
  assignmentTypeId: number;
}

export interface AddAssignment {
  assignmentName: string;
  description: string;
  startDate: string;
  endDate: string;
  isArchive: boolean;
  assignmentTypeId: number;
}

export interface AssignmentRequest {
  first: number;
  rows: number;
  sortField: string | string[];
  sortOrder: number;
  filter?: {
    AssignmentName: string;
  };
}
