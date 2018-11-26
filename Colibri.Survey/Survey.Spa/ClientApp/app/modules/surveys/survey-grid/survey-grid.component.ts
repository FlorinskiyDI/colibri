

import { Component } from '@angular/core';
import { SurveyModel } from '../../../shared/models/form-builder/survey.model';
import { SurveysApiService } from '../../../shared/services/api/surveys.api.service';
import { Router } from '@angular/router';
import { PortalApiService } from 'shared/services/api/portal.api.service';
@Component({
    selector: 'survey-grid',
    templateUrl: './survey-grid.component.html',
    styleUrls: [
        './survey-grid.component.scss'
    ],
    // template: `<router-outlet></router-outlet>`
})
export class SurveyGridComponent {

    surveys: SurveyModel[];

    constructor(
        private router: Router,
        private portalService: PortalApiService,
        private surveysApiService: SurveysApiService,
    ) { }

    ngOnInit() {
        this.surveysApiService.getAll().subscribe((data: SurveyModel[]) => {
            this.surveys = data;
            console.log(data);
        });
    }


    GoTo(link: any) {
        this.router.navigateByUrl(link);
    }


    GetExcelReport(id: any) {
        this.portalService.getExcelReport(id).subscribe((response: any) => {
            // save file as xlsx
            const link = document.createElement('a');
            const file = new Blob([response._body]);
            link.href = window.URL.createObjectURL(file);
            link.download = 'name.xlsx';
            link.click();
            console.log('got excel report successfull!');
        });
    }
}


