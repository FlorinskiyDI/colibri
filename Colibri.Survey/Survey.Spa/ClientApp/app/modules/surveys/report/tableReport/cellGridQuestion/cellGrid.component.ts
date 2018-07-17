import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
// import { SurveysApiService } from '../shared/services/api/surveys.api.service';
// import { SurveyModel } from '../shared/models/form-builder/survey.model';

// import { PortalApiService } from 'shared/services/api/portal.api.service';


@Component({
    selector: 'app-cellGrid',
    templateUrl: 'cellGrid.component.html',
    // styleUrls: ['./tableReport.component.scss'],
    encapsulation: ViewEncapsulation.None,
})

export class CellGridComponent implements OnInit {

    @Input() rowData: any;
    cellsOfRow: any[] = [];


    constructor() {

    }



    ngOnInit() {
        debugger
        // this.rowData.forEach((item: any) => {
        //     if (item.inputTypeName === 'GridRadio') {
        //         item.answer.forEach((element: any) => {
        //             this.cellsOfRow.push(element.answer);
        //         });
        //     } else {
        //         this.cellsOfRow.push(item.answer);
        //     }
        // });
    }
}
