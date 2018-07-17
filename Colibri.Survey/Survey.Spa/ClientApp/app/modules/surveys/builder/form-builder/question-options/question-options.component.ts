import { Component, ViewEncapsulation, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';


import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionService } from '../../services/builder.service';

import { ControTypes } from 'shared/constants/control-types.constant';
import { ControStates } from 'shared/constants/control-states.constant';

@Component({
    selector: 'question-options',
    templateUrl: './question-options.component.html',
    styleUrls: [
        './question-options.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})

export class QuestionOptionsComponent {

    @Input() questionTypes: any = {};

    divRequied = false;
    divAdditional = false;
    question: any;
    control: any;
    types: any[] = [];
    selectedType = '';
    newquestion: any;
    questionOption: any = null;
    questionControl: FormGroup;

    constructor(
        public questionService: QuestionService,
        private questionTransferService: QuestionTransferService,

    ) {

        this.questionTransferService.getQuestionOption().subscribe((data: any) => {
            if (data != null) {
                this.question = data.question;
                this.control = data.control;
                this.selectedType = data.question.controlType;
                this.questionOption = data.question;
                this.questionControl = data.control;
            } else {
                this.questionOption = null;
                this.questionControl = null;
            }
        });
        this.questionTransferService.getFlickerOption().subscribe((id: number) => {
            if (id === 1) {
                this.divRequied = true;
                setTimeout(() => {
                    this.divRequied = false;
                }, 500); // time as transition property in css
            }
            if (id === 2) {
                this.divAdditional = true;
                setTimeout(() => {
                    this.divAdditional = false;
                }, 500); // time as transition property in css
            }

        });
    }


    changeQuestion(event: any) {
        const group: any = {};
        this.getQuestionByType(event.value, this.question.order);
        this.newquestion.id = this.question.id;
        this.newquestion.state = ControStates.created;
        const contrl = this.questionService.addTypeAnswer(this.newquestion, group) as FormGroup;

        this.questionTransferService.setDataForChangeQuestion({
            control: contrl,
            object: this.newquestion
        });
    }

    ngOnInit() {
        this.questionTypes.questionTemplates.forEach((item: any) => {
            this.types.push({ label: item.name, value: item.type, icon: item.icon });
        });
    }


    changeQuestionValidation(question: any, state: boolean) {
        question.state = question.state !== ControStates.created ? ControStates.updated : question.state;
    }


    AddAdditionalQuestion(question: any, state: boolean) {
        debugger
        question.state = question.state !== ControStates.created ? ControStates.updated : question.state;
        // if (!state) {
        //     this.questionControl.controls['additionalAnswer'].setValue('');
        // }
    }



    getQuestionByType(value: any, index: any) {
        this.newquestion = {};
        switch (value) {
            case ControTypes.textbox: {
                this.newquestion = this.questionService.getTextboxControl(index);
                break;
            }
            case ControTypes.textarea: {
                this.newquestion = this.questionService.getTextareaControl(index);
                break;
            }
            case ControTypes.radio: {
                this.newquestion = this.questionService.getRadioButtonControl(index);
                break;
            }
            case ControTypes.checkbox: {
                this.newquestion = this.questionService.getCheckBoxControl(index);
                break;
            }
            case ControTypes.dropdown: {
                this.newquestion = this.questionService.getDropdownControl(index);
                break;
            }
            case ControTypes.gridRadio: {
                this.newquestion = this.questionService.getGridRadioControl(index);
                break;
            }
            default: {
                console.log('Invalid choice');
                break;
            }
        }
    }
}
