import { Component, OnInit } from '@angular/core';

import { PortalApiService } from 'shared/services/api/portal.api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-tableReport',
    templateUrl: 'tableReport.component.html',
    styleUrls: ['./tableReport.component.scss']
})

export class TableReportComponent implements OnInit {

    result: any = null;
    surveyId: string;
    public answerList: any[];
    columOptions: any[];

    gridLoading: boolean;
    constructor(
        private portalService: PortalApiService,
        private route: ActivatedRoute,
    ) {
        this.route.params.subscribe((params: any) => {

            this.surveyId = params['id'] ? params['id'] : null;
        });

        this.gridLoading = true;
    }


    ngOnInit() {
        this.portalService.getAnswers(this.surveyId).subscribe((data: any) => {
            console.log('1111111111111111111111111111111111111111111111111');
            console.log(data.spentTimeMs);
            console.log(data.spentTimeSec);
            console.log('1111111111111111111111111111111111111111111111111');

            this.answerList = data.answers;
            this.columOptions = data.headerOption;
            this.gridLoading = false;
        });
    }


    customSort(event: any) {
        console.log('work work');
        event.data.sort((data1: any, data2: any) => {
            const value1 = data1.find((x: any) => x.questionName === event.field).answer;
            const value2 = data2.find((x: any) => x.questionName === event.field).answer;

            if (value1 == null && value2 != null) {
                this.result = -1;
            } else
                if (value1 != null && value2 == null) {
                    this.result = 1;
                } else
                    if (value1 == null && value2 == null) {
                        this.result = 0;
                    } else
                        if (typeof value1 === 'string' && typeof value2 === 'string') {
                            this.result = value1.localeCompare(value2);
                        } else {
                            this.result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;
                        }

            const result = event.order * this.result;
            return (result);
        });
    }
}
