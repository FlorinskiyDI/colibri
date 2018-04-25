import { Component, Input, OnInit, OnChanges, EventEmitter, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
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
    providers: [QuestionControlService]
})
export class SurveyFormBuilderComponent implements OnInit, OnChanges {
    @Input() questionSettings: any;
    @Input() questions: QuestionBase<any>[] = [];
    @Input() question: QuestionBase<any>;
    form: FormGroup;

    editQuestionKey = '';

    @Output()
    temporaryAnser: EventEmitter<any> = new EventEmitter<any>();
    payLoad = '';
    newquestion: any;

    constructor(
        private questionTransferService: QuestionTransferService,
        private qcs: QuestionControlService,
        public questionControlService: QuestionControlService,
        // private fb: FormBuilder
    ) {
        this.questionTransferService.getDropQuestion().subscribe((data: any) => {
            console.log('some question wass deleted');
            console.log(data);


            // remove question
            this.form.removeControl(data.key);
            console.log(this.form);
            this.sortQuestionByIndex();
        });
    }

    sortQuestionByIndex() {
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });
    }

    setQuestionOption(question: any, checked: boolean) {
        if (checked) {
            this.questionTransferService.setQuestionOption(
                {
                    question : question,
                    control : this.form.controls[question.key]
                }
            );
            console.log('send option to rightbar!');
            console.log(checked);
        }
        console.log('turn off edit state!');

    }

    editQuestionEvent(item: any) {
        console.log(item.cheked);
    }

    ngOnInit() {
        this.form = this.qcs.toFormGroup(this.questions);
        console.log('0000000000000000000000000000000000000000000000');
        console.log(this.form.valid);
        console.log(this.form);
        console.log('0000000000000000000000000000000000000000000000');
        // debugger
    }


    ngOnChanges(changes: any) {

        // this.doSomething(changes.categoryId.currentValue);

    }


    changeQuestionOrders(item: any, index: number) {
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });
    }


    addNewQuestion($event: any, index: number) {
        // debugger
        // organisere question orden
        this.questions.forEach(x => {
            const indexOf = this.questions.indexOf(x);
            x.order = indexOf;
        });

        this.questions.splice(index, 1); // remove AvailableQuestions object


        console.log('101010101010101010101010101010101010101010101010101010101010101010101');
        console.log($event.dragData);
        console.log(index);
        console.log('101010101010101010101010101010101010101010101010101010101010101010101');

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


        // this.form.addControl(this.newquestion.key, this.fb.group({
        //     'answer': !this.newquestion.required ? new FormControl(this.newquestion.value || '') : new FormControl(this.newquestion.value || '', Validators.required),
        //     'additionalAnswer': new FormControl('')
        // }));
        // console.log('0000000000000000000000000000000000000000000000');
        // console.log(this.form.valid);
        // console.log(this.form);
        // console.log('0000000000000000000000000000000000000000000000');
        const group: any = {};
        this.form.addControl(this.newquestion.key, this.questionControlService.addTypeAnswer(this.newquestion, group)
        );

        // console.log('0000000000000000000000000000000000000000000000');
        // console.log(this.form.valid);
        // console.log(this.form);
        // console.log('0000000000000000000000000000000000000000000000');

        this.questions.push(this.newquestion);
        this.questions.sort((a, b) => a.order - b.order);
    }


    onDragEnd8() {
        console.log('8888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888888');
    }
    onDragEnd1(event: any) {
        console.log('3333333333333333333333311111111133333333333333333333333333333333333333333333333333333333333333333333333');
        console.log(event);
        console.log('3333333333333333333333311111111133333333333333333333333333333333333333333333333333333333333333333333333');
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
