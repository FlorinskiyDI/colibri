import { Component, Input, OnInit, EventEmitter, Output, AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ControTypes } from '../../../../shared/constants/control-types.constant';

import { QuestionTransferService } from '../../../../shared/transfers/question-transfer.service';
import { QuestionControlService } from '../../../../shared/services/question-control.service';
// import { AvailableQuestions } from '../../../../shared/models/form-builder/form-control/available-question.model';
// import { DropdownQuestion } from '../../../../shared/Models/form-builder/question-dropdown.model';
import { QuestionBase } from '../../../../shared/Models/form-builder/question-base.model';
// import { TextboxQuestion } from '../../../../shared/Models/form-builder/question-textbox.model';
// import { CheckboxQuestion } from '../../../../shared/Models/form-builder/question-checkbox.model';


@Component({
    selector: 'survey-from-builder',
    templateUrl: './survey-form-builder.component.html',
    styleUrls: ['./survey-form-builder.component.scss'],
    providers: [QuestionControlService],
    // changeDetection: ChangeDetectionStrategy.Default,
})
export class SurveyFormBuilderComponent implements OnInit, AfterContentChecked {
    @Input() questionSettings: any;
    @Input() questions: QuestionBase<any>[] = [];
    @Input() question: QuestionBase<any>;
    form: FormGroup;

    pageId: any;
    editQuestionKey = '';

    @Output()
    temporaryAnser: EventEmitter<any> = new EventEmitter<any>();
    payLoad = '';
    newquestion: any;

    CheckedOptQuestion: string;

    constructor(
        private cdr: ChangeDetectorRef,
        private questionTransferService: QuestionTransferService,
        private qcs: QuestionControlService,
        public questionControlService: QuestionControlService,
        private fb: FormBuilder
    ) {
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
        this.questionTransferService.getQuestions().subscribe((page: any) => { // updata formbuild after select page

            console.log('55555555555555555555555');
            console.log('55555555555555555555555');

            console.log(this.form);
            console.log('55555555555555555555555');
            console.log('55555555555555555555555');
            // this.form =  this.fb.group([]);
            // this.form = this.fb.group(page.id, this.qcs.toFormGroup(page.questions));
            debugger
        });
    }

    ngOnInit() {
        debugger
        this.pageId = 'id_page1';
        // const item: any = {};
        const page: any = {};
        page[this.pageId] = this.qcs.toFormGroup(this.questions);
        // item[this.pageId] = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));
        // this.form = item;
        // this.form.addControl(this.pageId, this.qcs.toFormGroup(this.questions));
        // this.form = this.fb.group(this.pageId, this.qcs.toFormGroup(this.questions));




        this.form = new FormGroup(page);




        
        // this.form = this.qcs.toFormGroup(this.questions);
        debugger
        
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
                    control: this.form.controls[question.id]
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
        switch ($event.dragData.type) {
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
        const group: any = {};
        this.form.addControl(this.newquestion.id, this.questionControlService.addTypeAnswer(this.newquestion, group)
        );

        this.questions.push(this.newquestion);
        this.questions.sort((a, b) => a.order - b.order);
    }


    onSubmit() {
        this.payLoad = this.form.value;
        this.temporaryAnser.emit(this.payLoad);
    }



    ngAfterContentChecked() {

        this.cdr.detectChanges();
    }
}
