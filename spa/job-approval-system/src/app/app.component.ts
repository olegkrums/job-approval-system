import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { PositionStatusApi } from './service/job-approval-api.service';
import { IJobSheetTableData, JobSheet, JobApprovalDecision } from './model/job-approval.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {

  private jobSheetData: IJobSheetTableData[] = [];

  public jobSheet: JobSheet = new JobSheet();
  public jobApprovalDecision: JobApprovalDecision = new JobApprovalDecision();

  constructor(private positionStatusApi: PositionStatusApi) {

  }

  displayedColumns: string[] = ['itemName', 'unitTime', 'unitCost'];
  dataSource = new MatTableDataSource(this.jobSheetData);

  @ViewChild(MatSort, { static: true }) sort: MatSort;

  getTotalCost() {
    return this.jobSheetData.map(t => t.unitCost).reduce((acc, value) => acc + value, 0);
  }

  approve() {
    this.positionStatusApi.approve(this.jobSheet);
  }

  ngOnInit() {
    this.dataSource.sort = this.sort;

    this.positionStatusApi.jobSheets.subscribe((jobSheet: any) => {
      this.jobSheet = jobSheet;

      let data: any = [];

      jobSheet.items.forEach(item => {
        data.push({ itemName: item.itemName, unitTime: item.itemTime, unitCost: item.unitCost });
      });

      this.dataSource = new MatTableDataSource(data);
    });


    this.positionStatusApi.jobApprovalDecision.subscribe((jobApprovalDecision: JobApprovalDecision) => {
      this.jobApprovalDecision = jobApprovalDecision;

      switch (jobApprovalDecision.jobApprovalDecisionEnum) {
        case 1:
          this.jobApprovalDecision.jobApprovalDecisionString = 'Approved';
          break;
        case 2:
          this.jobApprovalDecision.jobApprovalDecisionString = 'Refered';
          break;
        case 3:
          this.jobApprovalDecision.jobApprovalDecisionString = 'Declined';
          break;
        default:
            this.jobApprovalDecision.jobApprovalDecisionString = 'Unknown';
          break;
      }

    });

    let number = Math.floor(Math.random() * 3) + 1;
    this.positionStatusApi.getjobSheet('00000000-0000-0000-0000-00000000000' + number);
  }
}