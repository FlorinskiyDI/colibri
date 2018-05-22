import { Component, ViewEncapsulation } from '@angular/core';

// import { QuestionBase } from '../../../shared/models/form-builder/question-base.model';

import { ControTypes } from '../../../shared/constants/control-types.constant';
import { AvailableQuestions } from '../../../shared/models/form-builder/form-control/available-question.model';
import { SurveyModel } from '../../../shared/models/form-builder/survey.model';
import { ActivatedRoute } from '@angular/router';

// import { TextboxQuestion } from '../../../shared/models/form-builder/question-textbox.model';

// helpers
import { CompareObject } from '../../../shared/helpers/compare-object.helper';
import { isEqual, reduce } from 'lodash';

import { PageModel } from '../../../shared/models/form-builder/page.model';

import { SurveysApiService } from '../../../shared/services/api/surveys.api.service';
import { QuestionService } from '../../../shared/services/api/question.service';
import { QuestionControlService } from '../../../shared/services/question-control.service';

import { QuestionTransferService } from '../../../shared/transfers/question-transfer.service';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
// import { SurveyModule } from 'modules/surveys/survey.module';
// import { PageModel } from 'shared/models/form-builder/page.model';


@Component({
    selector: 'survey-builder-cmp',
    templateUrl: './survey-builder.component.html',
    styleUrls: [
        './survey-builder.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
    providers: [QuestionService, QuestionControlService]
})
export class SurveyBuilderComponent {

    oldSurvey: SurveyModel = null;
    activeSurveyId: string;
    page: any;
    pageinglist: any[];
    questions: QuestionBase<any>[];
    newquestion: any;
    questionOption: any = {};
    survey: SurveyModel = null;
    temporaryAnser: any;
    // availableQuestions: string[] = [
    //     ControTypes.checkbox,
    //     ControTypes.dropdown,
    //     ControTypes.gridRadio,
    //     ControTypes.radio,
    //     ControTypes.textarea,
    //     ControTypes.textbox
    // ];

    availableQuestions: Array<AvailableQuestions> = [

        new AvailableQuestions(
            ControTypes.textbox, 'Textbox', 1, 'dropZonesName1', 'Textbox description', 'fa-font'),
        new AvailableQuestions(ControTypes.textarea, 'Textarea', 2, 'dropZonesName2', 'Texarea description', 'fa-text-width'),
        new AvailableQuestions(ControTypes.radio, 'Radiogroup', 3, 'dropZonesName3', 'Radio group description', 'fa-dot-circle-o'),
        new AvailableQuestions(ControTypes.checkbox, 'Checkbox', 4, 'dropZonesName4', 'Checkbox description', 'fa-check-square-o'),
        new AvailableQuestions(ControTypes.dropdown, 'Dropdown', 5, 'dropZonesName5', 'Dropdown description', 'fa-indent'),
        new AvailableQuestions(ControTypes.gridRadio, 'Grid (single choice)', 6, 'dropZonesName6', 'grid description', 'fa-table'),


    ];

    availableQuestionsOption: any;








    listOne: Array<string> = ['Coffee', 'Orange Juice', 'Red Wine', 'Unhealty drink!', 'Water'];
    listTeamOne: Array<string> = [];



    constructor(
        private route: ActivatedRoute,
        private surveysApiService: SurveysApiService,
        private questionTransferService: QuestionTransferService,
        public questionService: QuestionService,
        public questionControlService: QuestionControlService
    ) {
        this.route.params.subscribe((params: any) => {
            this.activeSurveyId = params['id'];
            console.log('5555555555555555555555555555555555555555555555555555555555555');
            console.log(this.activeSurveyId);
            console.log('5555555555555555555555555555555555555555555555555555555555555');
        });
        this.questionTransferService.getIdByNewPage().subscribe((id: any) => {
            const page = new PageModel({
                id: id,
                name: 'page name',
                description: 'page description',
                order: 10,
                questions: []
            });
            this.survey.pages.push(page);
            this.questionTransferService.setQuestions(page);
        });

        this.questionTransferService.getdeletePageId().subscribe((data: any) => {
            console.log('delete page delete page delete page delete page' + data.id);

            const deletepage = this.survey.pages.find(x => x.id === data.id);
            const index = this.survey.pages.indexOf(deletepage);
            this.survey.pages.splice(index, 1);

            this.pageinglist = this.generatePagingList(this.survey.pages);
            console.log('change paginglist');

            let page = null;
            if (data.index > 0) {
                page = this.survey.pages[data.index - 1];
                this.questionTransferService.setQuestions(page);
            } else {
                page = this.survey.pages[0];
                this.questionTransferService.setQuestions(page);
            }

            // this.page = page;


        });




    }

    ngOnInit() {
        this.availableQuestionsOption = {
            allowquestion: ['dropZonesName1', 'dropZonesName2', 'dropZonesName3', 'dropZonesName4', 'dropZonesName5', 'dropZonesName6'],
            availableQuestions: [
                { type: ControTypes.textbox, name: 'Textbox', order: 1, dropZonesName: 'dropZonesName1', description: 'Textbox description', icon: 'fa-font' } as AvailableQuestions,
                { type: ControTypes.textarea, name: 'Textarea', order: 2, dropZonesName: 'dropZonesName2', description: 'Texarea description', icon: 'fa-text-width' } as AvailableQuestions,
                { type: ControTypes.radio, name: 'Radiogroup', order: 3, dropZonesName: 'dropZonesName3', description: 'Radio group description', icon: 'fa-dot-circle-o' } as AvailableQuestions,
                { type: ControTypes.checkbox, name: 'Checkbox', order: 4, dropZonesName: 'dropZonesName4', description: 'Checkbox description', icon: 'fa-check-square-o' } as AvailableQuestions,
                { type: ControTypes.dropdown, name: 'Dropdown', order: 5, dropZonesName: 'dropZonesName5', description: 'Dropdown description', icon: 'fa-indent' } as AvailableQuestions,
                { type: ControTypes.gridRadio, name: 'Grid (single choice)', order: 6, dropZonesName: 'dropZonesName6', description: 'grid description', icon: 'fa-table' } as AvailableQuestions
            ],
        };



        if (this.activeSurveyId === 'create') {
            this.survey = this.questionService.getSurvey();
            if (this.survey.pages) {
                this.page = this.survey.pages[0];
                // this.questions = this.survey.pages[0].questions;
                this.pageinglist = this.generatePagingList(this.survey.pages);
            }
        } else {
            this.surveysApiService.get(this.activeSurveyId).subscribe((data: SurveyModel) => {

                this.oldSurvey = data;

                this.survey = data;
                if (this.survey.pages) {
                    this.page = data.pages[0];
                    // this.questions = this.survey.pages[0].questions;
                    this.pageinglist = this.generatePagingList(data.pages);
                }
            });
        }

        // if (this.survey.pages) {
        //     this.page = this.survey.pages[0];
        //     // this.questions = this.survey.pages[0].questions;
        //     this.pageinglist = this.generatePagingList(this.survey.pages);
        // }
    }

    compareCheck() {
        console.log('---------------------------------------------------------------------------------');
        console.log(this.oldSurvey);
        console.log(this.survey);
        console.log('---------------------------------------------------------------------------------');
        const a = {
            same: 1,
            different: 2,
            missing_from_b: 3,
            missing_nested_from_b: {
                x: 1,
                y: 2
            },
            nested: {
                same: 1,
                different: 2,
                missing_from_b: 3
            }
        };

        const b = {
            same: 1,
            different: 99,
            missing_from_a: 3,
            missing_nested_from_a: {
                x: 1,
                y: 2
            },
            nested: {
                same: 1,
                different: 99,
                missing_from_a: 3
            }
        };
        const val = this.compare(a, b);
        debugger
        console.log(val);
    }


    compare = function (a: any, b: any) {

        const  result: any = {
            different: [],
            missing_from_first: [],
            missing_from_second: []
        };

        _.reduce(a, function (result, value, key) {
            if (b.hasOwnProperty(key)) {
                if (_.isEqual(value, b[key])) {
                    return result;
                } else {
                    if (typeof (a[key]) !== typeof ({}) || typeof (b[key]) !== typeof ({})) {
                        // dead end.
                        result.different.push(key);
                        return result;
                    } else {
                        const deeper = this.compare(a[key], b[key]);
                        result.different = result.different.concat(_.map(deeper.different, (sub_path) => {
                            return key + '.' + sub_path;
                        }));

                        result.missing_from_second = result.missing_from_second.concat(_.map(deeper.missing_from_second, (sub_path) => {
                            return key + '.' + sub_path;
                        }));

                        result.missing_from_first = result.missing_from_first.concat(_.map(deeper.missing_from_first, (sub_path) => {
                            return key + '.' + sub_path;
                        }));
                        return result;
                    }
                }
            } else {
                result.missing_from_second.push(key);
                return result;
            }
        }.bind(this), result);

        _.reduce(b, function (result, value, key) {
            if (a.hasOwnProperty(key)) {
                return result;
            } else {
                result.missing_from_first.push(key);
                return result;
            }
        }, result);

        return result;
    };

    GetQuestionByPage(id: any) {
        console.log('work work work work work work work  111111111111111');
        const page = this.survey.pages.find(item => item.id === id);
        this.questionTransferService.setQuestions(page);
        // this.page = page;
        // this.questions = page.questions;
    }


    // changeData() {
    //     console.log('change data chanage dada');

    //     this.questions = this.questionService.getQuestions();
    // }

    generatePagingList(pages: any[]) {
        const result: any[] = [];
        pages.forEach((item: any, index: number) => {
            result.push({ title: 'Page', id: item.id });
        });
        return result;
    }


    getQuestonDropzones() {
        const dropZoneList: any[] = [];
        this.availableQuestions.forEach(val => {
            dropZoneList.push(val.dropZonesName);
        });
        return dropZoneList;
    }

    getSurvey() {
        this.surveysApiService.get(this.survey.id).subscribe((data: SurveyModel) => {
            console.log(this.survey);
            console.log(data as SurveyModel);
            this.survey = data as SurveyModel;
        });
    }

    removeDragQuestion(question: any) {
        console.log('remove control remove control remove control remove control remove control remove control remove control');
        this.questionTransferService.setDropQuestion(question);
    }


    getTestAnswer(answer: any) {
        // const data = this.surveysApiService.save(this.survey);
        const data = this.surveysApiService.update(this.survey);
        console.log(data);
    }


    onDragEnd8(event: any, data: any) {
        console.log('88888888888888888888899999999999999999999999');
        console.log(event);
        console.log(data);
        console.log('88888888888888888888899999999999999999999999');

        this.availableQuestions.push(data);
        this.availableQuestions.sort((a: any, b: any) => a.order - b.order);
        this.questionTransferService.setDropQuestionId(data);
    }


    dragOperation = false;





    listBoxers: Array<string> = ['Sugar Ray Robinson',
        'Muhammad Ali', 'George Foreman', 'Joe Frazier', 'Jake LaMotta', 'Joe Louis', 'Jack Dempsey', 'Rocky Marciano', 'Mike Tyson', 'Oscar De La Hoya'];

    listTeamTwo: Array<string> = [];


}









// evailabal question list
// class AvailableQuestions {
//     constructor(public name: string, public order: number, public dropZonesName: string) { }
// }
