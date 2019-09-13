import { Injectable } from '@angular/core'
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { map } from 'rxjs/operators';

import { JobSheet, JobApprovalDecision } from '../model/job-approval.model';

@Injectable()
export class JobApprovalApi {
    private _dataStore: {
        jobSheet: JobSheet,
        jobApprovalDecision: JobApprovalDecision,
    };

    private _jobSheet: Subject<JobSheet>;
    private _jobApprovalDecision: Subject<JobApprovalDecision>;

    constructor(private httpClient: HttpClient) {

        this._jobSheet = new Subject<JobSheet>();
        this._jobApprovalDecision = new Subject<JobApprovalDecision>();

        this._dataStore = {
            jobSheet: new JobSheet(),
            jobApprovalDecision: new JobApprovalDecision(),
        };

        this._dataStore.jobSheet = new JobSheet();
        this._dataStore.jobApprovalDecision = new JobApprovalDecision();
    }

    public get jobSheets() {
        return this._jobSheet.asObservable();
    }

    public get jobApprovalDecision() {
        return this._jobApprovalDecision.asObservable();
    }

    public getjobSheet(sheetId: string): any {

        this.httpClient.get('https://localhost:44382/api/JobApproval/GetJobSheet/' + sheetId, {
            responseType: 'json',
        }).pipe(map(res => res)).subscribe((data: JobSheet) => {

            if (data) {
                this._dataStore.jobSheet = data;
                this._jobSheet.next(Object.assign({}, this._dataStore).jobSheet);
            }
        }, (data: any) => {
            alert(data.message);
        });
    }


    public approve(jobSheet: JobSheet): any {

        this.httpClient.post('https://localhost:44382/api/JobApproval/ApproveJobSheet/', {
            'id': jobSheet.id,
            'items': jobSheet.items,
            'referenceHoursInMin': jobSheet.referenceHoursInMin,
            'referenceTotalPrice': jobSheet.referenceTotalPrice,
            'laborHourCost': jobSheet.laborHourCost
        }).pipe(map(res => res)).subscribe((data: JobApprovalDecision) => {

            if (data) {
                this._dataStore.jobApprovalDecision = data;
                this._jobApprovalDecision.next(Object.assign({}, this._dataStore).jobApprovalDecision);
            }
        }, (data: any) => {
            alert(data.message);
        });
    }
}