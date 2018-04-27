import { Component, Input, AfterContentChecked, ChangeDetectorRef, ChangeDetectionStrategy } from '@angular/core';
import { FormControl, FormGroup, FormArray, FormBuilder } from '@angular/forms';

import { QuestionBase } from '../../../../../shared/models/form-builder/question-base.model';
import { CheckboxQuestion } from '../../../../../shared/models/form-builder/question-checkbox.model';
import { ControlOptionModel } from '../../../../../shared/models/form-builder/form-control/control-option.model';
import { GUID } from '../../../../../shared/helpers/guide-type.helper';
@Component({
    selector: 'survey-form-question',
    templateUrl: './survey-form-question.component.html',
    styleUrls: ['./survey-form-question.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyFormQuestionComponent implements AfterContentChecked {

    lastSelectRowId: any;

    @Input() question: QuestionBase<any>;
    @Input() form: FormGroup;
    @Input() isEditQuestion: boolean;

    get isValid() {
        return this.form.controls[this.question.id].valid;
    }
    get isDirty() { return this.form.controls[this.question.id].dirty; }


    constructor(
        private cdr: ChangeDetectorRef,
        private fb: FormBuilder) { }


    onChange(questonId: string, optionId: string, isChecked: boolean, index: number) {
        const checkboxquestion = this.question as CheckboxQuestion;
        const option = checkboxquestion.options.find((x: ControlOptionModel) => x.id === optionId);
        option.label = isChecked;
        // const answer: any = 'answer';
        const checkBoxArray = <FormArray>this.form.controls[questonId];
        const checkBoxControl = checkBoxArray.controls['answer'];
        console.log(this.question);
        if (isChecked) {
            checkBoxControl.push(new FormControl(optionId));
        } else {
            index = checkBoxControl.controls.findIndex((x: any) => x.value === optionId);
            checkBoxControl.removeAt(index);
        }
    }


    // setOption(event: any, key: string, input: any) {
    //     this.form.controls[key].setValue = input.value;
    //     console.log(input.value);
    //     console.log(event.target.value);
    // }


    addItem(mass: any[]) {
        const item = new ControlOptionModel(GUID.getNewGUIDString(), '', 'your text...', 1);
        mass.push(item);
    }

    addRow(mass: any, questionId: string) {
        const groupRows: any = {};
        const rowId = GUID.getNewGUIDString(); // !!! user twice, (formbuildr, json)
        groupRows[rowId] = this.fb.group({
            'label': new FormControl('')
        });

        const questionArray = this.form.controls[questionId] as FormGroup;
        const constRows = questionArray.controls['rows'] as FormGroup;
        constRows.controls[GUID.getNewGUIDString()] = this.fb.group(groupRows);

        const item = new ControlOptionModel(rowId, '', 'your text...', 1);
        mass.push(item);
    }



    addCol(mass: any) {
        const item = new ControlOptionModel(GUID.getNewGUIDString(), '', 'your text...', 1);
        mass.push(item);
    }

    deleteItem(index: number, mass: ControlOptionModel[]) {
        mass.splice(index, 1);
    }


    // onChange(key: string, label: string, isChecked: boolean) {
    //     console.log(key);
    //     const checkBoxArray = <FormArray>this.form.controls[key];
    //     let checkBoxControl = checkBoxArray.controls['answer'];
    //     checkBoxControl = this.fb.array([]);
    //     console.log(checkBoxArray);
    //     if (isChecked) {
    //       checkBoxControl.push(new FormControl(label));
    //     } else {
    //       const index = checkBoxControl.controls.findIndex(x => x.value === label)
    //       checkBoxControl.removeAt(index);
    //     }
    //   }



    onChangeGridRadio(itemRowLabel: any, itemCol: any, key: string, label: string, isChecked: boolean) {
        const radioArray = <FormArray>this.form.controls[key];
        const answer = 'answer';
        const answerControl = radioArray.controls[answer];



        const group: any = {};



        group['row'] = this.fb.group({
            'id': new FormControl(itemRowLabel.id),
            'label': new FormControl(itemRowLabel.value)
        });
        group['col'] = this.fb.group({
            'id': new FormControl(itemCol.id),
            'label': new FormControl(itemCol.value)
        });


        const item2 = answerControl.controls.findIndex((x: any) => x.controls['row'].controls['id'].value === itemRowLabel.id);
        const needcontrol = answerControl.controls[item2];

        if (!!needcontrol) {
            answerControl.removeAt(item2);
        }


        // let index = answerControl.controls.findIndex(x => x.id == itemRowLabel.id)
        // // let index = group.controls.findIndex(x => x.id == itemRowLabel.id)
        // answerControl.removeAt(this.lastSelectRowId);

        const formGroup = new FormGroup(group);
        answerControl.push(formGroup);
        this.lastSelectRowId = formGroup;

        // answerControl = [];
        //   if (isChecked && this.lastSelectRowId == '' || this.lastSelectRowId == itemRowLabel.id ) {
        //     console.log('workkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk');

        //   } else {

        //     // let index = answerControl.controls.findIndex(x => x.id == itemCol.id)
        //     // checkBoxControl.re.removeAt(index);
        //   }


        console.log('000000000000000000000000000');
        console.log(radioArray);
        console.log('000000000000000000000000000');

    }

    ngAfterContentChecked() {

        this.cdr.detectChanges();
    }
}
