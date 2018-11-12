import { Component, OnInit } from '@angular/core';
// import { SurveysApiService } from '../shared/services/api/surveys.api.service';
// import { SurveyModel } from '../shared/models/form-builder/survey.model';

import { PortalApiService } from 'shared/services/api/portal.api.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-tableReport',
    templateUrl: 'tableReport.component.html',
    styleUrls: ['./tableReport.component.scss'],
    // encapsulation: ViewEncapsulation.None,
})

export class TableReportComponent implements OnInit {

    // surveys: SurveyModel[];
    cols: any[];
    result: any = null;
    public cars3: any[] = [];
    surveyId: string;
    public message = '111111111111111111111';
    public answerList: any[];
    columOptions: any[];
    sales: any[];


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
            console.log('1111111111111111111111111111111111111111111111111');
            console.log('1111111111111111111111111111111111111111111111111');
            console.log(data.spentTimeMs);
            console.log(data.spentTimeSec);
            console.log('1111111111111111111111111111111111111111111111111');
            console.log('1111111111111111111111111111111111111111111111111');
            console.log('1111111111111111111111111111111111111111111111111');

            this.answerList = data.answers;
            this.columOptions = data.headerOption;
            this.gridLoading = false;
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

        this.cars3 = [
            {
                vin: 'dsad231ff',
                year: 2012,
                brand: 'VW',
                color: 'Orange'
            },
            {
                vin: 'gwregre345',
                year: 2011,
                brand: 'Audi',
                color: 'Black'
            },
            {
                vin: 'h354htr',
                year: 2005,
                brand: 'Renault',
                color: 'Gray'
            },
            {
                vin: 'j6w54qgh',
                year: 2003,
                brand: 'BMW',
                color: 'Blue'
            },
            {
                vin: 'hrtwy34',
                year: 1995,
                brand: 'Mercedes',
                color: 'Orange'
            },
        ];


        this.cols = [
            { field: 'vin', header: 'Vin' },
            { field: 'year', header: 'Year' },
            { field: 'brand', header: 'Brand' },
            { field: 'color', header: 'Color' }
        ];
    }





    customSort1(event: any) {
        console.log('work work');
        event.data.sort((data1: any, data2: any) => {
            const value1 = data1.find((x: any) => x.questionName === event.field).answer;
            const value2 = data2.find((x: any) => x.questionName === event.field).answer;
            // const result = null;

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

    customSort2(event: any) {
        console.log('work work');
        event.data.sort((data1: any, data2: any) => {
            const value1 = data1[event.field];
            const value2 = data2[event.field];
            // const result = null;

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
