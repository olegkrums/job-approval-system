export interface IJobSheetTableData {
  itemName: string;
  unitTime: number;
  unitCost: number;
}

export class JobSheet {
  id: string = '';
  items: Item[] = [];
  referenceHoursInMin: number = 0;
  referenceTotalPrice: number = 0;
  unitTime: number = 0;
  unitCost: number = 0;
  laborHourCost: number = 0;
  totalCost: number = 0;
}

export class Item {
  itemId: string = '';
  itemName: string = '';
  genericCategory: string = '';
  itemTime: number = 0;
  unitCost: number = 0;
}

export class JobApprovalDecision {
  jobApprovalDecisionEnum: number = 0;
  reasonForDeclining: string = '';
  //UI
  jobApprovalDecisionString: string = '';
}
