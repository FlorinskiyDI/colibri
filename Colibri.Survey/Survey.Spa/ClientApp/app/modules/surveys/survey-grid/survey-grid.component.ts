

import { Component } from '@angular/core';
import { SurveyModel } from '../../../shared/models/form-builder/survey.model';
import { SurveysApiService } from '../../../shared/services/api/surveys.api.service';
import { Router } from '@angular/router';
import { PortalApiService } from 'shared/services/api/portal.api.service';
import { MessageService } from 'primeng/components/common/messageservice';

@Component({
    selector: 'survey-grid',
    templateUrl: './survey-grid.component.html',
    styleUrls: [
        './survey-grid.component.scss'
    ],
    providers: [

        MessageService
    ]
    // template: `<router-outlet></router-outlet>`
})
export class SurveyGridComponent {

    surveys: SurveyModel[];

    constructor(
        private messageService: MessageService,
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
            link.download = 'surveyReport.xlsx';
            link.click();
            console.log('got excel report successfull!');
        });
    }


    ChangeLock(surveyId: any, isLocked: boolean) {
        this.surveysApiService.changeLock(surveyId, !isLocked).subscribe((response: any) => {

            const survey = this.surveys.find(x => x.id === surveyId);
            survey.isLocked = !isLocked;
            this.messageService.add({ severity: 'success', summary: 'Success', detail: ('The survey: "' + survey.name + '" was ' + (survey.isLocked ? 'locked' : 'unlocked')) });
        });
    }
}


