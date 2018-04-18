import { Component, Input, OnInit, OnChanges, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ControTypes } from '../../../../shared/constants/control-types.constant';

import { QuestionControlService } from '../../../../shared/services/question-control.service';
import { AvailableQuestions } from '../../../../shared/models/form-builder/form-control/available-question.model';
// import { DropdownQuestion } from '../../../../shared/Models/form-builder/question-dropdown.model';
import { QuestionBase } from '../../../../shared/Models/form-builder/question-base.model';
// import { TextboxQuestion } from '../../../../shared/Models/form-builder/question-textbox.model';
// import { CheckboxQuestion } from '../../../../shared/Models/form-builder/question-checkbox.model';


@Component({
    selector: 'survey-from-builder',
    templateUrl: './survey-form-builder.component.html',
    styleUrls: ['./survey-form-builder.component.scss'],
    providers: [QuestionControlService]
})
export class SurveyFormBuilderComponent implements OnInit, OnChanges {
    count = 0;
    @Input() questionSettings: any;
    @Input() questions: QuestionBase<any>[] = [];
    @Input() question: QuestionBase<any>;
    form: FormGroup;

    @Output()
    temporaryAnser: EventEmitter<any> = new EventEmitter<any>();
    payLoad = '';
    newquestion: any;

    constructor(
        private qcs: QuestionControlService,
        public questionControlService: QuestionControlService,
        private fb: FormBuilder
    ) {
        // console.log(questionSettings);
    }

    ngOnInit() {
        this.form = this.qcs.toFormGroup(this.questions);
        console.log('101010101010101010101010101010101010101010101010101010101010101010101');
        console.log(this.questionSettings);
        console.log('101010101010101010101010101010101010101010101010101010101010101010101');
    }


    ngOnChanges(changes: any) {

        // this.doSomething(changes.categoryId.currentValue);

    }


    addNewQuestion($event: any) {
        const index = this.questions.find((x: any) => !!x.name  );
        if(index as AvailableQuestions)
        this.questions.removeAt(index);
        // this.questions.findIndex

        debugger
        switch ($event.dragData.name) {
            case ControTypes.textbox: {
                this.newquestion = this.questionControlService.addTextboxControl();
                console.log(ControTypes.textbox);
                break;
            }

            case ControTypes.textarea: {
                console.log(ControTypes.textarea);
                break;
            }

            case ControTypes.radio: {
                console.log(ControTypes.radio);
                break;
            }

            case ControTypes.checkbox: {
                console.log(ControTypes.checkbox);
                break;
            }
            case ControTypes.checkbox: {
                console.log(ControTypes.dropdown);
                break;
            }
            case ControTypes.checkbox: {
                console.log(ControTypes.gridRadio);
                break;
            }

            default: {
                console.log('Invalid choice');
                break;
            }
        }
        this.form.addControl(this.newquestion.key, this.fb.group({
            'answer': this.newquestion.required ? new FormControl(this.newquestion.value || '') : new FormControl(this.newquestion.value || '', Validators.required),
            'additionalAnswer': new FormControl('')
        }));
        this.count++;
        this.questions.push(this.newquestion);
    }

    onDragEnd1() {
        console.log('onDragEndonDragEndonDragEndonDragEndonDragEndonDragEndonDragEnd');
    }


    // addNewQuestionEmail(valReque: boolean) {
    //     const key = 'Email' + this.count;
    //     const quest = new TextboxQuestion({
    //         key: key,
    //         label: 'Email' + this.count,
    //         type: 'email',
    //         order: 2,
    //         required: valReque,
    //         isAdditionalAnswer: false
    //     });

    //     this.form.addControl(key, this.fb.group({
    //         'answer': quest.required ? new FormControl(quest.value || '') : new FormControl(quest.value || '', Validators.required),
    //         'additionalAnswer': new FormControl('')
    //     }));
    //     this.count++;
    //     this.questions.push(quest);
    // }



    // addNewQuestionDrop(valReque: boolean) {
    //     const key = 'DropDown' + this.count;
    //     const questDrop = new DropdownQuestion({
    //         key: key,
    //         label: 'Bravery Rating',
    //         options: [
    //             { key: 'solid', value: 'Solid' },
    //             { key: 'great', value: 'Great' },
    //             { key: 'good', value: 'Good' },
    //             { key: 'unproven', value: 'Unproven' }
    //         ],
    //         order: 5,
    //         required: valReque,
    //         isAdditionalAnswer: false
    //     });
    //     this.form.addControl(key, this.fb.group({
    //         'answer': questDrop.required ? new FormControl(questDrop.value || '') : new FormControl(questDrop.value || '', Validators.required),
    //         'additionalAnswer': new FormControl('')
    //     }));
    //     this.count++;
    //     this.questions.push(questDrop);
    // }


    // addNewQuestionCheckbox(valReque: boolean, isAdditional: boolean) {

    //     const key = 'Checkbox' + this.count;
    //     const questCheckbox = new CheckboxQuestion({
    //         key: key,
    //         label: 'checkbox' + this.count,
    //         options: [
    //             { key: 'opt_11', label: 'Option 11', value: false },
    //             { key: 'opt_22', label: 'Option 22', value: false }
    //         ],
    //         order: 6,
    //         required: valReque,
    //         isAdditionalAnswer: isAdditional
    //     });

    //     this.form.addControl(key,
    //         this.fb.group({
    //             'answer': questCheckbox.required ? this.fb.array([], Validators.required) : this.fb.array([]),
    //             'additionalAnswer': new FormControl('')
    //         })

    //     );
    //     this.form.addControl(key, questCheckbox.required ? this.fb.array([], Validators.required) : this.fb.array([]));
    //     this.count++;
    //     this.questions.push(questCheckbox);
    // }

    // addNewRowsForCheckbox0() {
    //     const question = this.questions.find((val) => val.key === 'Checkbox0') as CheckboxQuestion;
    //     question.options.push({ key: 'opt_33', value: false, label: 'Option 33' });
    // }


    // changeValidForEmail0() {
    //     console.log(this.form.controls['Email0'].valid);
    //     this.form.controls['Email0'].clearValidators();
    //     this.form.get('Email0').updateValueAndValidity();
    // }

    onSubmit() {
        this.payLoad = this.form.value;
        this.temporaryAnser.emit(this.payLoad);
    }
}
