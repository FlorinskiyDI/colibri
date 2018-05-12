import { Component, Input, OnInit, EventEmitter, Output, AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ControTypes } from 'shared/constants/control-types.constant';

import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionControlService } from 'shared/services/question-control.service';
// import { AvailableQuestions } from 'shared/models/form-builder/form-control/available-question.model';
// import { DropdownQuestion } from 'shared/models/form-builder/question-dropdown.model';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
// import { TextboxQuestion } from 'shared/models/form-builder/question-textbox.model';
// import { CheckboxQuestion } from 'shared/models/form-builder/question-checkbox.model';


@Component({
    selector: 'survey-from-builder',
    templateUrl: './survey-form-builder.component.html',
    styleUrls: ['./survey-form-builder.component.scss'],
    providers: [QuestionControlService],
    // changeDetection: ChangeDetectionStrategy.Default,
})
export class SurveyFormBuilderComponent implements OnInit, AfterContentChecked {
    @Input() questionSettings: any;
    @Input() page: any = {};

    @Input() question: QuestionBase<any>;
    form: FormGroup;

    existPagesId: string[] = [];
    questions: QuestionBase<any>[] = [];
    pageId: any;
    editQuestionKey = '';

    @Output()
    temporaryAnser: EventEmitter<any> = new EventEmitter<any>();
    payLoad = '';
    newquestion: any;

    CheckedOptQuestion: string;

    constructor(
        private fb: FormBuilder,
        private cdr: ChangeDetectorRef,
        private questionTransferService: QuestionTransferService,
        // private qcs: QuestionControlService,
        public questionControlService: QuestionControlService,

    ) {
        this.questionTransferService.getDataForChangeQuestion().subscribe((data: any) => {
    

            debugger
            // this.questions.forEach(function (item: any, i: number) {
            //     if (i === data.index) {
            //         item = data.object;
            //     }
            // });
            data.object.id = this.questions[data.index].id;
            this.questions[data.index] = data.object;

            const val = this.form.controls[this.page.id] as FormGroup;
            // val.controls[data.id] = data.control;

            this.setQuestionOption(data.object, true);

        });
        this.questionTransferService.getDropQuestion().subscribe((data: any) => {
            // remove question
            this.form.removeControl(data.id);
            this.sortQuestionByIndex();
        });
        this.questionTransferService.getDropQuestionId().subscribe((data: any) => { // check drag control if lost focus without need area
            this.questions.forEach((item: any, index: number) => {
                const value = item as QuestionBase<any>;
                if (!value.id) {
                    this.questions.splice(index, 1);
                }
            });
        });
        this.questionTransferService.getdeletePageId().subscribe((data: any) => {
            this.form.removeControl(data.id);
        });
        this.questionTransferService.getQuestions().subscribe((page: any) => { // updata formbuild after select page


            const questionList = page.questions;
            this.pageId = page.id; // before toFormGroup();
            const pageId = this.existPagesId.find(x => x === page.id);
            if (!pageId) {
                const data: any = {};
                data[this.pageId] = this.questionControlService.toFormGroup(questionList);
                this.existPagesId.push(this.pageId);
                // item[this.pageId] = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));
                // this.form = item;
                // this.form.addControl(this.pageId, this.qcs.toFormGroup(this.questions));
                // this.form = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));

                // this.form = new FormGroup(data);
                // const dataPage = this.form.controls[this.page.id] as FormGroup;
                this.form.addControl(this.pageId, this.questionControlService.toFormGroup(questionList));
            }

            this.page = page;
            this.questions = page.questions;
        });
    }

    ngOnInit() {
        console.log('11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111______22222');
        this.questions = this.page.questions;
        this.pageId = this.page.id; // before toFormGroup();
        // const item: any = {};
        const page: any = {};
        page[this.pageId] = this.questionControlService.toFormGroup(this.questions);
        this.existPagesId.push(this.pageId);
        // item[this.pageId] = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));
        // this.form = item;
        // this.form.addControl(this.pageId, this.qcs.toFormGroup(this.questions));
        // this.form = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));
        this.form = new FormGroup(page);





        // this.form = this.qcs.toFormGroup(this.questions);


    }

    sortQuestionByIndex() {
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });
    }

    setQuestionOption(question: any, checked: boolean) {
        this.CheckedOptQuestion = question.id;
        if (checked) {
            this.questionTransferService.setQuestionOption(
                {
                    question: question,
                    control: this.form.get(this.page.id).get(question.id)
                }
            );
        } else {
            this.questionTransferService.setQuestionOption(null);
        }

    }

    changeQuestionOrders(item: any, index: number) {
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });
    }


    flickerNotificationField(id: number) {
        this.questionTransferService.setFlickerOption(id);
    }

    addNewQuestion($event: any, index: number) {
        // organisere question orden
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });

        this.questions.splice(index, 1); // remove AvailableQuestions object
        this.getQuestionByType($event.dragData.type, index);
        const group: any = {};
        const dataPage = this.form.controls[this.page.id] as FormGroup;
        dataPage.addControl(this.newquestion.id, this.questionControlService.addTypeAnswer(this.newquestion, group)
        );

        this.questions.push(this.newquestion);
        this.questions.sort((a, b) => a.order - b.order);
    }




    getQuestionByType(value: any, index: any) {
        switch (value) {
            case ControTypes.textbox: {
                this.newquestion = this.questionControlService.addTextboxControl(index);
                break;
            }
            case ControTypes.textarea: {
                this.newquestion = this.questionControlService.addTextareaControl(index);
                break;
            }
            case ControTypes.radio: {
                this.newquestion = this.questionControlService.addRadioButtonControl(index);
                break;
            }
            case ControTypes.checkbox: {
                this.newquestion = this.questionControlService.addCheckBoxControl(index);
                break;
            }
            case ControTypes.dropdown: {
                this.newquestion = this.questionControlService.addDropdownControl(index);
                break;
            }
            case ControTypes.gridRadio: {
                this.newquestion = this.questionControlService.addGridRadioControl(index);
                break;
            }

            default: {
                console.log('Invalid choice');
                break;
            }
        }
    }


    onSubmit() {
        this.payLoad = this.form.value;
        this.temporaryAnser.emit(this.payLoad);
    }



    ngAfterContentChecked() {
        this.form.updateValueAndValidity();
        this.cdr.detectChanges();
    }
}
