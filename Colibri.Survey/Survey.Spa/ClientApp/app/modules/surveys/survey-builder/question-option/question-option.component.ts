import { Component, ViewEncapsulation, Input } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';

import { QuestionControlService } from '../../../../shared/services/question-control.service';
// import { GridRadioQuestion } from '../../../../shared/models/form-builder/question-base.model';
import { QuestionTransferService } from '../../../../shared/transfers/question-transfer.service';
import { ControTypes } from '../../../../shared/constants/control-types.constant';
// import { FormControl } from '@angular/forms/src/model';
@Component({
    selector: 'question-option',
    templateUrl: './question-option.component.html',
    styleUrls: [
        './question-option.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})
export class QuestionOptionComponent {

    divRequied = false;
    divAdditional = false;
    question: any;
    control: any;
    types: any[] = [];
    selectedType = '';

    newquestion: any;

    @Input() questionTypes: any = {};


    questionOption: any = null;
    questionControl: FormGroup;
    constructor(
        // public questionControlService: QuestionControlService,
        public questionService: QuestionControlService,
        private questionTransferService: QuestionTransferService,
        // private fb: FormBuilder
    ) {




        // this.types = [
        //     { label: 'Audi', value: 'Audi', icon:  },
        //     { label: 'BMW', value: 'BMW' },
        //     { label: 'Fiat', value: 'Fiat' },
        //     { label: 'Ford', value: 'Ford' },
        //     { label: 'Honda', value: 'Honda' },
        //     { label: 'Jaguar', value: 'Jaguar' },
        //     { label: 'Mercedes', value: 'Mercedes' },
        //     { label: 'Renault', value: 'Renault' },
        //     { label: 'VW', value: 'VW' },
        //     { label: 'Volvo', value: 'Volvo' }
        // ];





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


    changeQuestion() {
        debugger
        console.log(this.selectedType);
        const group: any = {};

        const geterateQuestion: any = this.getQuestionByType(this.selectedType, 1);
        this.questionService.addTypeAnswer(geterateQuestion, group);
        // this.control = this.newquestion;
    }

    ngOnInit() {

        this.questionTypes.availableQuestions.forEach((item: any) => {
            this.types.push({ label: item.name, value: item.type, icon: item.icon });
        });
    }


    changeQuestionValidation(state: boolean) {

        console.log(this.questionOption);
        switch (this.questionOption.controlType) {

            case ControTypes.gridRadio: {

                const gridQuestion = this.questionOption;
                const rowsGroup: any = this.questionControl.controls['rows'];
                gridQuestion.grid.rows.forEach((item: any) => {
                    if (state) {
                        console.log('set require grid');
                        rowsGroup.controls[item.id].controls['label'].setValidators(Validators.required);
                        rowsGroup.controls[item.id].controls['label'].updateValueAndValidity();
                    } else {
                        console.log('delete require grid');
                        rowsGroup.controls[item.id].controls['label'].clearValidators();
                        rowsGroup.controls[item.id].controls['label'].updateValueAndValidity();
                    }
                });
                break;
            }
            default: {
                if (state) {

                    this.questionControl.controls['answer'].setValidators(Validators.required);
                    this.questionControl.controls['answer'].updateValueAndValidity();
                } else {
                    this.questionControl.controls['answer'].clearValidators();
                    this.questionControl.controls['answer'].updateValueAndValidity();
                }
            }
        }


    }

    HideOption(type: string) {
        if (type !== ControTypes.textbox && type !== ControTypes.textarea) {
            return true;
        } else {
            return false;
        }
    }

    AddAdditionalQuestion(state: boolean) {
        if (!state) {
            this.questionControl.controls['additionalAnswer'].setValue('');
        }
    }






    getQuestionByType(value: any, index: any) {
        switch (value) {
            case ControTypes.textbox: {
                this.newquestion = this.questionService.addTextboxControl(index);
                break;
            }
            case ControTypes.textarea: {
                this.newquestion = this.questionService.addTextareaControl(index);
                break;
            }
            case ControTypes.radio: {
                this.newquestion = this.questionService.addRadioButtonControl(index);
                break;
            }
            case ControTypes.checkbox: {
                this.newquestion = this.questionService.addCheckBoxControl(index);
                break;
            }
            case ControTypes.dropdown: {
                this.newquestion = this.questionService.addDropdownControl(index);
                break;
            }
            case ControTypes.gridRadio: {
                this.newquestion = this.questionService.addGridRadioControl(index);
                break;
            }

            default: {
                console.log('Invalid choice');
                break;
            }
        }
    }
}
