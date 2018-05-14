import { Component, ViewEncapsulation } from '@angular/core';

// import { QuestionBase } from '../../../shared/models/form-builder/question-base.model';

import { ControTypes } from '../../../shared/constants/control-types.constant';
import { AvailableQuestions } from '../../../shared/models/form-builder/form-control/available-question.model';
import { SurveyModel } from '../../../shared/models/form-builder/survey.model';

import { PageModel } from '../../../shared/models/form-builder/page.model';

import { QuestionService } from '../../../shared/services/api/question.service';
import { QuestionControlService } from '../../../shared/services/question-control.service';

import { QuestionTransferService } from '../../../shared/transfers/question-transfer.service';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
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

    page: any = {};
    pageinglist: any[];
    questions: QuestionBase<any>[];
    newquestion: any;
    questionOption: any = {};
    survey: SurveyModel;
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
        private questionTransferService: QuestionTransferService,
        public questionService: QuestionService,
        public questionControlService: QuestionControlService
    ) {
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

        this.survey = this.questionService.getSurvey();

        if (this.survey.pages) {
            this.page = this.survey.pages[0];
            // this.questions = this.survey.pages[0].questions;
            this.pageinglist = this.generatePagingList(this.survey.pages);
        }
    }


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



    removeDragQuestion(question: any) {
        this.questionTransferService.setDropQuestion(question);
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
