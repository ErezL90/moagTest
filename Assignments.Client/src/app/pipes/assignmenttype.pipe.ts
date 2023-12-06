import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'assignmentType',
})
export class AssignmentTypePipe implements PipeTransform {
  transform(assignmentTypeId: number, assignmentTypes: any[]): string {
    const assignmentType = assignmentTypes.find(
      (type) => type.assignmentTypeId === assignmentTypeId
    );
    return assignmentType ? assignmentType.assignmentTypeName : 'Unknown Type';
  }
}
