

import { Component } from '@angular/core';
import { SurveyModel } from '../../../shared/models/form-builder/survey.model';
import { SurveysApiService } from '../../../shared/services/api/surveys.api.service';
import { Router } from '@angular/router';
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
}


