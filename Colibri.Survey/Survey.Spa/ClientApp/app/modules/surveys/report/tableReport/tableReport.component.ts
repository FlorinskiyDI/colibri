import { Component, OnInit, ViewEncapsulation } from '@angular/core';
// import { SurveysApiService } from '../shared/services/api/surveys.api.service';
// import { SurveyModel } from '../shared/models/form-builder/survey.model';

import { PortalApiService } from 'shared/services/api/portal.api.service';


@Component({
    selector: 'app-tableReport',
    templateUrl: 'tableReport.component.html',
    styleUrls: ['./tableReport.component.scss'],
    encapsulation: ViewEncapsulation.None,
})

export class TableReportComponent implements OnInit {

    // surveys: SurveyModel[];
    public message = '111111111111111111111';
    public answerList: any[];
    columOptions: any[];
    sales: any[];
    constructor(
        private portalService: PortalApiService

    ) {

    }



    ngOnInit() {
        this.portalService.getAnswers('75ddba4f-1089-e811-8388-107b44194709').subscribe((data: any) => {
            console.log("answerList");
            console.log("answerList");
            console.log(data);
            debugger
            this.answerList = data.answers;
            this.columOptions = data.headerOption;
        });
        this.sales = [
            { brand: 'Apple', lastYearSale: '51%', thisYearSale: '40%', lastYearProfit: '$54,406.00', thisYearProfit: '$43,342', extend: { first: 1, two: 11 } },
            { brand: 'Samsung', lastYearSale: '83%', thisYearSale: '96%', lastYearProfit: '$423,132', thisYearProfit: '$312,122', extend: { first: 2, two: 22 } },
            { brand: 'Microsoft', lastYearSale: '38%', thisYearSale: '5%', lastYearProfit: '$12,321', thisYearProfit: '$8,500', extend: { first: 3, two: 33 } },
            { brand: 'Philips', lastYearSale: '49%', thisYearSale: '22%', lastYearProfit: '$745,232', thisYearProfit: '$650,323,', extend: { first: 4, two: 44 } },
            { brand: 'Song', lastYearSale: '17%', thisYearSale: '79%', lastYearProfit: '$643,242', thisYearProfit: '500,332', extend: { first: 5, two: 55 } },
            { brand: 'LG', lastYearSale: '52%', thisYearSale: ' 65%', lastYearProfit: '$421,132', thisYearProfit: '$150,005', extend: { first: 6, two: 66 } },
            { brand: 'Sharp', lastYearSale: '82%', thisYearSale: '12%', lastYearProfit: '$131,211', thisYearProfit: '$100,214', extend: { first: 7, two: 77 } },
            { brand: 'Panasonic', lastYearSale: '44%', thisYearSale: '45%', lastYearProfit: '$66,442', thisYearProfit: '$53,322', extend: { first: 8, two: 88 } },
            { brand: 'HTC', lastYearSale: '90%', thisYearSale: '56%', lastYearProfit: '$765,442', thisYearProfit: '$296,232', extend: { first: 9, two: 99 } }
        ];
    }
}
