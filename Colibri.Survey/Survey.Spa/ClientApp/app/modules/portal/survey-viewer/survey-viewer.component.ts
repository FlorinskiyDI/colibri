
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';


import { PortalApiService } from 'shared/services/api/portal.api.service';
import { SurveyModel } from 'shared/models/form-builder/survey.model';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { FormGroup } from '@angular/forms';
import { AnswerModel } from 'shared/models/portal//answer.model';


@Component({
    selector: 'survey-viewer',
    templateUrl: './survey-viewer.component.html',
    styleUrls: [
        './survey-viewer.component.scss'
    ],
    // template: `<router-outlet></router-outlet>`
})
export class SurveyViewerComponent implements OnInit {

    errorList: FormGroup = null;
    survey: SurveyModel = null;
    page: any;
    activeSurveyId: any;
    pageinglist: any[];


    constructor(
        private route: ActivatedRoute,
        private portalApiService: PortalApiService,
        private questionTransferService: QuestionTransferService,
    ) {
        this.route.params.subscribe((params: any) => {
            this.activeSurveyId = params['id'];
        });
        window.scroll(0, 0);

        this.questionTransferService.getViewerPage().subscribe((pageId: string) => {
            // Init data
            window.scroll(0, 0);

            this.initPage(pageId);
            // this.page = this.survey.pages.find(item => item.id === pageId);
            // this.questionTransferService.setFormViewrPage(this.page);
            // this.form.addControl(page.id, this.questionService.getFormPageGroup(page));
        });
    }



    initPage(pageId: string) {
        this.page = this.survey.pages.find(item => item.id === pageId);
        this.questionTransferService.setFormViewrPage(this.page);
    }


    ngOnInit() {
        this.portalApiService.getSurvey(this.activeSurveyId).subscribe((data: SurveyModel) => {
            this.survey = data;
            if (this.survey.pages) {
                this.page = data.pages[0];
                // this.questions = this.survey.pages[0].questions;
                this.pageinglist = this.generatePagingList(data.pages);
            }
        });
    }



    generatePagingList(pages: any[]) {
        const result: any[] = [];
        pages.forEach((item: any, index: number) => {
            result.push({ title: 'Page', id: item.id, index: index });
        });
        return result;
    }


    sendAnswers(data: FormGroup) {
        this.errorList = data;
        if (data.valid) {
            // const question = data.controls[controlPageName] as FormGroup;
            const answers: any[] = [];
            Object.keys(data.controls)
                .forEach(controlName => {
                    const question = data.controls[controlName] as FormGroup;
                    Object.keys(question.controls)
                        .forEach(controlQuestionName => {

                            const val = {
                                id: controlQuestionName,
                                type: question.controls[controlQuestionName].get('type').value,
                                answer: question.controls[controlQuestionName].get('answer').value,
                                additionalAnswer: question.controls[controlQuestionName].get('additionalAnswer').value
                            } as AnswerModel;
                            answers.push(val);

                        });
                });
                const respondentData = {
                    SurveyId: this.survey.id,
                    AnswerList: answers
                };
            this.portalApiService.saveAnswers(JSON.stringify(respondentData)).subscribe((response: any) => {
                debugger
                console.log('111111111111111111111111111111111111111');
            });
        } else {
            Object.keys(data.controls)
                .forEach(controlName => {

                    if (data.controls[controlName].valid) {
                        console.log(data.controls[controlName].valid);
                    }
                });
        }
    }
}
