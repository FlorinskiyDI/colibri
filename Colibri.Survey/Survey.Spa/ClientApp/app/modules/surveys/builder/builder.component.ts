import { Component, ViewEncapsulation } from '@angular/core';

import { ControTypes } from 'shared/constants/control-types.constant';
import { ControStates } from 'shared/constants/control-states.constant';

import { SurveyModel } from 'shared/models/form-builder/survey.model';
import { Router, ActivatedRoute } from '@angular/router';

// helpers
import { cloneDeep } from 'lodash';

import { PageModel } from 'shared/models/form-builder/page.model';

import { QuestionService } from 'shared/services/api/question.service';
import { QuestionControlService } from 'shared/services/question-control.service';
import { SurveysApiService } from 'shared/services/api/surveys.api.service';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';

import { FormBuilderComponent } from './form-builder/form-builder.component';

import { QuestionTemplate } from 'shared/models/form-builder/form-control/question-template.model';
// import { forEach } from '@angular/router/src/utils/collection';

import { FormGroup } from '@angular/forms';

@Component({
    selector: 'survey-builder',
    templateUrl: './builder.component.html',
    styleUrls: [
        './builder.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
    providers: [QuestionService, QuestionControlService]
})


export class BuilderComponent {

    page: PageModel;
    survey: SurveyModel;
    questions: QuestionBase<any>[];

    templateOptions: any;
    formPages: FormGroup[];
    surveyId: any;

    pagingList: any[] = [];
    questionTemplates: any[];
    isValidForm = true;
    deletePageList: string[] = [];

    constructor(
        private router: Router,
        private surveysApiService: SurveysApiService,
        private route: ActivatedRoute,
        private questionTransferService: QuestionTransferService,
        public questionService: QuestionService,
    ) {

        this.route.params.subscribe((params: any) => {

            this.surveyId = params['id'] ? params['id'] : null;
        });

        this.questionTransferService.getSelectedPage().subscribe((pageId: string) => {
            // Init data
            this.page = this.survey.pages.find(item => item.id === pageId);
            this.questionTransferService.setFormPage(this.page);
        });

        this.questionTransferService.getPageById().subscribe((id: any) => {

            const page = new PageModel({
                id: id,
                name: 'page name',
                description: 'page description',
                order: this.survey.pages.length,
                questions: [],
                state: ControStates.created
            });
            this.survey.pages.push(page);

            this.page = page;
            this.questionTransferService.setFormPage(page);
        });

        this.questionTransferService.getdeletePageId().subscribe((data: any) => {
            this.deletePageList.push(data.id);
            this.survey.pages.splice(data.index, 1);
            this.page = data.index > 1 ? this.survey.pages[data.index - 1] : this.survey.pages[0];
            this.questionTransferService.setFormPage(this.page);
            this.sortPagesByIndex();


        });
    }

    ngOnInit() {
        this.templateOptions = {
            dragTemplateZones: ['dropZone1', 'dropZone2', 'dropZone3', 'dropZons4', 'dropZone5', 'dropZone6'],
            questionTemplates: cloneDeep(this.getTemplates())
        };



        if (this.surveyId === null) {

            this.survey = this.questionService.getSurvey();
            this.pagingList = this.getPagingList();
            // init data
            this.page = this.survey.pages[0];
            this.questionTemplates = this.getTemplates();

        } else {
            this.surveysApiService.get(this.surveyId).subscribe((data: SurveyModel) => {

                this.survey = data;

                if (this.survey.pages) {
                    this.page = data.pages[0];
                    this.pagingList = this.getPagingList();
                    this.questionTemplates = this.getTemplates();
                }
            });
        }
    }


    sortPagesByIndex() {
        this.survey.pages.forEach(x => {
            const indexOf = this.survey.pages.indexOf(x);
            x.order = indexOf;
        });
    }

    checkstate(formState: boolean) {
        this.isValidForm = formState;
    }

    dragEndQuestionTemplate(event: any, widget: any) { // add back to template list drag question
        console.log('11111111111111111111111111');
        this.questionTemplates.push(widget);
        this.questionTemplates.sort((a: any, b: any) => a.order - b.order);
        this.questionTransferService.setQuestionForDelete(widget);
    }


    deleteDragQuestion(question: any) {
        // this.deleteQuestionList.push(question.id);
        this.questionTransferService.setDeleteDragQuestion(question);
    }


    getTemplates(): any[] {
        return [
            new QuestionTemplate(ControTypes.textbox, 'Textbox', 1, 'dropZone1', 'Textbox description', 'fa-font'),
            new QuestionTemplate(ControTypes.textarea, 'Textarea', 2, 'dropZone2', 'Texarea description', 'fa-text-width'),
            new QuestionTemplate(ControTypes.radio, 'Radiogroup', 3, 'dropZone3', 'Radio group description', 'fa-dot-circle-o'),
            new QuestionTemplate(ControTypes.checkbox, 'Checkbox', 4, 'dropZons4', 'Checkbox description', 'fa-check-square-o'),
            new QuestionTemplate(ControTypes.dropdown, 'Dropdown', 5, 'dropZone5', 'Dropdown description', 'fa-indent'),
            new QuestionTemplate(ControTypes.gridRadio, 'Grid (single choice)', 6, 'dropZone6', 'grid description', 'fa-table'),
        ];
    }


    getPagingList(): any[] {
        const result: any[] = [];
        this.survey.pages.forEach((item: any, index: number) => {
            result.push({ title: 'Page', id: item.id });
        });
        return result;
    }


    saveSurvey() {
        if (this.surveyId === null) {
            const data = this.surveysApiService.save(this.survey);

        } else {
            const updateData: any = {
                survey: this.survey,
                // deleteQuestions: this.deleteQuestionList
                deleteQuestions: FormBuilderComponent.deleteQuestionList,
                deletePages: this.deletePageList
            };

            const data = this.surveysApiService.update(updateData);

            debugger
            this.router.navigateByUrl('/surveys');
        }


    }
}

