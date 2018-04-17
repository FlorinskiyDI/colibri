import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';


import { QuestionControlService } from '../../../../shared/services/question-control.service';

import { DropdownQuestion } from '../../../../shared/Models/form-builder/question-dropdown.model';
import { QuestionBase } from '../../../../shared/Models/form-builder/question-base.model';
import { TextboxQuestion } from '../../../../shared/Models/form-builder/question-textbox.model';
import { CheckboxQuestion } from '../../../../shared/Models/form-builder/question-checkbox.model';


@Component({
    selector: 'survey-from-builder',
    templateUrl: './survey-form-builder.component.html',
    styleUrls: ['./survey-form-builder.component.scss'],
    providers: [QuestionControlService]
})
export class SurveyFormBuilderComponent implements OnInit {
    count = 0;
    @Input() questions: QuestionBase<any>[] = [];
    form: FormGroup;

    @Output()
    temporaryAnser: EventEmitter<any> = new EventEmitter<any>();
    payLoad = '';

    constructor(private qcs: QuestionControlService, private fb: FormBuilder) {
        console.log('111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111');
     }

    ngOnInit() {
        console.log('111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111');
        console.log(this.form);
        console.log('1111111111111111111111111');
        this.form = this.qcs.toFormGroup(this.questions);
        console.log('1111111111111111111111111');
        console.log(this.form);
        console.log('1111111111111111111111111');
    }


    addNewQuestionEmail(valReque: boolean) {
        const key = 'Email' + this.count;
        const quest = new TextboxQuestion({
            key: key,
            label: 'Email' + this.count,
            type: 'email',
            order: 2,
            required: valReque,
            isAdditionalAnswer: false
        });

        this.form.addControl(key, this.fb.group({
            'answer': quest.required ? new FormControl(quest.value || '') : new FormControl(quest.value || '', Validators.required),
            'additionalAnswer': new FormControl('')
        }));
        this.count++;
        this.questions.push(quest);
    }



    addNewQuestionDrop(valReque: boolean) {
        const key = 'DropDown' + this.count;
        const questDrop = new DropdownQuestion({
            key: key,
            label: 'Bravery Rating',
            options: [
                { key: 'solid', value: 'Solid' },
                { key: 'great', value: 'Great' },
                { key: 'good', value: 'Good' },
                { key: 'unproven', value: 'Unproven' }
            ],
            order: 5,
            required: valReque,
            isAdditionalAnswer: false
        });
        this.form.addControl(key, this.fb.group({
            'answer': questDrop.required ? new FormControl(questDrop.value || '') : new FormControl(questDrop.value || '', Validators.required),
            'additionalAnswer': new FormControl('')
        }));
        this.count++;
        this.questions.push(questDrop);
    }


    addNewQuestionCheckbox(valReque: boolean, isAdditional: boolean) {

        const key = 'Checkbox' + this.count;
        const questCheckbox = new CheckboxQuestion({
            key: key,
            label: 'checkbox' + this.count,
            options: [
                { key: 'opt_11', label: 'Option 11', value: false },
                { key: 'opt_22', label: 'Option 22', value: false }
            ],
            order: 6,
            required: valReque,
            isAdditionalAnswer: isAdditional
        });

        this.form.addControl(key,
            this.fb.group({
                'answer': questCheckbox.required ? this.fb.array([], Validators.required) : this.fb.array([]),
                'additionalAnswer': new FormControl('')
            })

        );
        this.form.addControl(key, questCheckbox.required ? this.fb.array([], Validators.required) : this.fb.array([]));
        this.count++;
        this.questions.push(questCheckbox);
    }

    addNewRowsForCheckbox0() {
        const question = this.questions.find((val) => val.key === 'Checkbox0') as CheckboxQuestion;
        question.options.push({ key: 'opt_33', value: false, label: 'Option 33' });
    }


    changeValidForEmail0() {
        console.log(this.form.controls['Email0'].valid);
        this.form.controls['Email0'].clearValidators();
        this.form.get('Email0').updateValueAndValidity();
    }

    onSubmit() {
        this.payLoad = this.form.value;
        this.temporaryAnser.emit(this.payLoad);
    }
}
