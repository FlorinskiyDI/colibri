import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { SurveysApiService } from '../shared/services/api/surveys.api.service';
import { SurveyModel } from '../shared/models/form-builder/survey.model';
@Component({
    selector: 'app-home',
    templateUrl: 'home.component.html',
    styleUrls: ['./home.component.scss'],
    encapsulation: ViewEncapsulation.None,
})

export class HomeComponent implements OnInit {

    surveys: SurveyModel[];
    public message: string;
    public values: any[];

    constructor(
        private surveysApiService: SurveysApiService,
    ) {

        this.surveysApiService.getAll().subscribe((data: SurveyModel[]) => {
            this.surveys = data;
            console.log(data);
        });
        this.message = 'HomeComponent constructor';
    }

    ngOnInit() {
    }
}
