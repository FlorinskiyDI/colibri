import { Component, ViewEncapsulation } from '@angular/core';

import { ControTypes } from 'shared/constants/control-types.constant';
import { AvailableQuestions } from 'shared/models/form-builder/form-control/available-question.model';

import { SurveyModel } from 'shared/models/form-builder/survey.model';
import { ActivatedRoute } from '@angular/router';

// helpers
import { CompareObject } from 'shared/helpers/compare-object.helper';
import { isEqual, reduce, cloneDeep } from 'lodash';

import { PageModel } from 'shared/models/form-builder/page.model';

import { SurveysApiService } from 'shared/services/api/surveys.api.service';
import { QuestionService } from 'shared/services/api/question.service';
import { QuestionControlService } from 'shared/services/question-control.service';

import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';

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

    questionTemplates: any[];



    constructor(
        public questionService: QuestionService,
    ) {
        this.questionTemplates = this.getTemplates();
    }

    ngOnInit() {
        this.templateOptions = {
            dragTemplateZones: ['dropZone1', 'dropZone2', 'dropZone3', 'dropZons4', 'dropZone5', 'dropZone6'],
            questionTemplates: cloneDeep(this.getTemplates())
        };
        this.survey = this.questionService.getSurvey();

        this.page = this.survey.pages[0];
        this.questions = this.survey.pages[0].questions;
    }



    dragEndQuestionTemplate(event: any, widget: any) {
        console.log('dragEndQuestionTemplate()');
    }


    deleteDragQuestion(event: any) {
        console.log('deleteDragQuestion()');
        // console.log(event);
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
}

