import { Injectable } from '@angular/core';
import { PageModel } from 'shared/models/form-builder/page.model';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { ControStates } from 'shared/constants/control-states.constant';
import { ControTypes } from 'shared/constants/control-types.constant';

import { GUID } from 'shared/helpers/guide-type.helper';
import { ControlOptionModel } from 'shared/models/form-builder/form-control/control-option.model';

import { QuestionBase } from 'shared/models/form-builder/question-base.model';
import { TextboxQuestion } from 'shared/models/form-builder/question-textbox.model';
import { DropdownQuestion } from 'shared/models/form-builder/question-dropdown.model';
import { TextAreaQuestion } from 'shared/models/form-builder/question-textarea.model';
import { GridRadioQuestion } from 'shared/models/form-builder/question-grid-radio.model';
import { RadioQuestion } from 'shared/models/form-builder/question-radio.model';
import { CheckboxQuestion } from 'shared/models/form-builder/question-checkbox.model';



@Injectable()
export class QuestionService {

    pageGroup: any = {};
    questionGroup: any = [];

    constructor(private fb: FormBuilder) { }


    getFormPageGroup(page: PageModel) {
        this.questionGroup = {};
        if (page.questions.length > 0) {
            page.questions.forEach((question: any) => {
                this.addTypeAnswer(question, this.questionGroup);
            });
        }
        return new FormGroup(this.questionGroup);
    }


    addTypeAnswer(question: QuestionBase<any>, group: any): any {

        switch (question.controlType) {
            case ControTypes.checkbox: {
                const options: any = {};
                question.options.forEach((item: any) => {
                    options[item.id] = this.fb.group({
                        'name': new FormControl(item.value, Validators.required)
                    });
                });
                group[question.id] = this.fb.group({
                    'name': new FormControl(question.text, Validators.required),
                    'description': new FormControl(question.description, Validators.required),
                    'options': this.fb.group(options)
                }) as FormGroup;

                this.markChangedQuestion(group[question.id], question);
                break;
            }

            case ControTypes.radio: {
                const options: any = {};
                question.options.forEach((item: any) => {
                    options[item.id] = this.fb.group({
                        'name': new FormControl(item.value, Validators.required)
                    });
                });
                group[question.id] = this.fb.group({
                    'name': new FormControl(question.text, Validators.required),
                    'description': new FormControl(question.description, Validators.required),
                    'options': this.fb.group(options)
                }) as FormGroup;

                this.markChangedQuestion(group[question.id], question);
                break;
            }

            case ControTypes.dropdown: {
                const options: any = {};
                question.options.forEach((item: any) => {
                    options[item.id] = this.fb.group({
                        'name': new FormControl(item.value, Validators.required)
                    });
                });
                group[question.id] = this.fb.group({
                    'name': new FormControl(question.text, Validators.required),
                    'description': new FormControl(question.description, Validators.required),
                    'options': this.fb.group(options)
                }) as FormGroup;

                this.markChangedQuestion(group[question.id], question);
                break;
            }

            case ControTypes.gridRadio: {
                const rows: any = {};
                const cols: any = {};
                const gridQuestion = question as GridRadioQuestion;
                gridQuestion.grid.rows.forEach((item: any) => {
                    rows[item.id] = this.fb.group({
                        'name': new FormControl(item.value, Validators.required)
                    });
                });
                gridQuestion.grid.cols.forEach((item: any) => {
                    cols[item.id] = this.fb.group({
                        'name': new FormControl(item.value, Validators.required)
                    });
                });
                group[question.id] = this.fb.group({
                    'name': new FormControl(question.text, Validators.required),
                    'description': new FormControl(question.description, Validators.required),
                    'rows': this.fb.group(rows),
                    'cols': this.fb.group(cols),
                }) as FormGroup;

                this.markChangedQuestion(group[question.id], question);
                break;
            }

            default: {
                group[question.id] = this.fb.group({
                    'name': new FormControl(question.text, Validators.required),
                    'description': new FormControl(question.description, Validators.required)
                }) as FormGroup;

                this.markChangedQuestion(group[question.id], question);
                break;
            }
        }
        return group[question.id];
    }

    markChangedQuestion(formControl: FormGroup, question: any) {
        // formControl.con.subscribe((form: any) => { // Intercept changes and mark the question for update

        //     if (!formControl.pristine) {
        //         question.state = question.state !== ControStates.created ? ControStates.updated : ControStates.created;
        //     }
        // });
        formControl.valueChanges.subscribe((form: any) => { // Intercept changes and mark the question for update
            if (!formControl.pristine) {
                question.state = question.state !== ControStates.created ? ControStates.updated : ControStates.created;
            }
        });
    }


    getTextboxControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new TextboxQuestion({
            value: '',
            id: id,
            text: 'Simple text ...',
            description: 'some description!',
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }


    getTextareaControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new TextAreaQuestion({
            value: '',
            id: id,
            text: 'Extended text ...',
            description: 'some description!',
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }


    getRadioButtonControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new RadioQuestion({
            id: id,
            text: 'Single choice ...',
            description: 'some description!',
            options: [
                new ControlOptionModel(GUID.getNewGUIDString(), '', 'radio 1', 0),
                new ControlOptionModel(GUID.getNewGUIDString(), '', 'radio 2', 1),
            ],
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }


    getCheckBoxControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new CheckboxQuestion({
            id: id,
            text: 'Multiple choice ...',
            description: 'some description!',
            options: [
                new ControlOptionModel(GUID.getNewGUIDString(), false, 'variable 1', 0),
                new ControlOptionModel(GUID.getNewGUIDString(), false, 'variable 2', 1),
            ],
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }


    getDropdownControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new DropdownQuestion({
            id: id,
            text: 'Select variable ...',
            description: 'some description!',
            options: [
                new ControlOptionModel(GUID.getNewGUIDString(), '1111', 'dropdown value 1', 0),
                new ControlOptionModel(GUID.getNewGUIDString(), '2222', 'dropdown value 2', 1)
            ],
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }

    getGridRadioControl(index: number): QuestionBase<any> {
        const id = GUID.getNewGUIDString(); // new guid
        const question = new GridRadioQuestion({
            id: id,
            text: 'Table with single row`s choice ...',
            description: 'Some description ...',
            grid: {
                cellInputType: 'radio',  // radio, checkbox
                rows: [
                    { id: 'id_question1', label: null, value: 'Variable question 1', order: 1 },
                    { id: 'id_question2', label: null, value: 'Variable question 2', order: 2 }
                ],
                cols: [
                    { id: 'id_answer1', label: null, value: 'answer 1', order: 1 },
                    { id: 'id_answer2', label: null, value: 'answer 2', order: 2 },
                ]
            },
            pageFlowModifier: false,
            order: index,
            required: false,
            isAdditionalAnswer: false
        });
        return question;
    }
}
