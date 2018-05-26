import { Component, Input, OnInit, EventEmitter, Output, AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ControTypes } from 'shared/constants/control-types.constant';

import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionService } from '../services/builder.service';
// import { AvailableQuestions } from 'shared/models/form-builder/form-control/available-question.model';
// import { DropdownQuestion } from 'shared/models/form-builder/question-dropdown.model';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
// import { TextboxQuestion } from 'shared/models/form-builder/question-textbox.model';
// import { CheckboxQuestion } from 'shared/models/form-builder/question-checkbox.model';

import { PageModel } from 'shared/models/form-builder/page.model';

@Component({
    selector: 'form-builder',
    templateUrl: './form-builder.component.html',
    styleUrls: ['./form-builder.component.scss'],

    providers: [QuestionService],
    // changeDetection: ChangeDetectionStrategy.Default,
})
export class FormBuilderComponent implements OnInit, AfterContentChecked {
    @Input() templateOptions: any;
    @Input() page: PageModel = new PageModel();

    formPage: FormGroup;

    selectQuestion: string;

    // @Output() temporaryAnser: EventEmitter<any> = new EventEmitter<any>();

    constructor(
        // private fb: FormBuilder,
        private cdr: ChangeDetectorRef,
        private questionTransferService: QuestionTransferService,
        // private qcs: QuestionControlService,
        public questionService: QuestionService,

    ) {

    }


    ngOnInit() {
        this.formPage = new FormGroup(this.questionService.getFormPageGroup(this.page));
        console.log(this.formPage.value);
    }


    setQuestionOption(question: any, checked: boolean) {
        this.selectQuestion = question.id;
        if (checked) {
            this.questionTransferService.setQuestionOption(
                {
                    question: question,
                    control: this.formPage.get(this.page.id).get(question.id)
                }
            );
        } else {
            this.questionTransferService.setQuestionOption(null);
        }
    }

    sortQuestionByIndex() {
        this.page.questions.forEach(x => {
            const indexOf = this.page.questions.indexOf(x);
            x.order = indexOf;
        });
    }

    addQuestion($event: any, index: number) {
        this.sortQuestionByIndex();
        this.page.questions.splice(index, 1); // remove AvailableQuestions object
        const question = this.getQuestionByType($event.dragData.type, index);
        const group: any = {};

        const dataPage = this.formPage.controls[this.page.id].get('questions') as FormGroup;
        dataPage.addControl(question.id, this.questionService.addTypeAnswer(question, group));

        this.page.questions.push(question);
        this.page.questions.sort((a, b) => a.order - b.order); // useles code
    }


    getQuestionByType(value: any, index: any) {
        switch (value) {
            case ControTypes.textbox: {
                return this.questionService.getTextboxControl(index);
            }
            case ControTypes.textarea: {
                return this.questionService.getTextareaControl(index);
            }
            case ControTypes.radio: {
                return this.questionService.getRadioButtonControl(index);
            }
            case ControTypes.checkbox: {
                return this.questionService.getCheckBoxControl(index);
            }
            case ControTypes.dropdown: {
                return this.questionService.getDropdownControl(index);
            }
            case ControTypes.gridRadio: {
                return this.questionService.getGridRadioControl(index);
            }

            default: {
                console.log('Invalid choice');
                return null;
            }
        }
    }



    ngAfterContentChecked() {
        // this.form.updateValueAndValidity();
        this.cdr.detectChanges();
    }
}
