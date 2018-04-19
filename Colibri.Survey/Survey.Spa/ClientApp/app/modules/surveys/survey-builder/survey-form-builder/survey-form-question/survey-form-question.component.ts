import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, FormArray, FormBuilder } from '@angular/forms';

import { QuestionBase } from '../../../../../shared/models/form-builder/question-base.model';

@Component({
    selector: 'survey-form-question',
    templateUrl: './survey-form-question.component.html',
    styleUrls: ['./survey-form-question.component.scss'],
})
export class SurveyFormQuestionComponent {

    lastSelectRowId: any;

    @Input() question: QuestionBase<any>;
    @Input() form: FormGroup;
    get isValid() {
        return this.form.controls[this.question.key].valid;
    }
    get isDirty() { return this.form.controls[this.question.key].dirty; }


    constructor(private fb: FormBuilder) { }


    onChange(key: string, label: string, isChecked: boolean) {
        debugger
        // const answer: any = 'answer';
        const checkBoxArray = <FormArray>this.form.controls[key];
        const checkBoxControl = checkBoxArray.controls['answer'];
        console.log(checkBoxArray);
        if (isChecked) {
            checkBoxControl.push(new FormControl(label));
        } else {
            const index = checkBoxControl.controls.findIndex((x: any) => x.value === label);
            checkBoxControl.removeAt(index);
        }
    }


    // onChange(key: string, label: string, isChecked: boolean) {
    //     debugger
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
            'label': new FormControl(itemRowLabel.label)
        });
        group['col'] = this.fb.group({
            'id': new FormControl(itemCol.id),
            'label': new FormControl(itemCol.label)
        });

        // let item2 = answerControl.controls.findIndex(x => x.controls['row'].controls['id'].value == itemRowLabel.id).;

        const item2 = answerControl.controls.findIndex((x: any) => x.controls['row'].controls['id'].value === itemRowLabel.id);
        const needcontrol = answerControl.controls[item2];


        // let item = answerControl.controls.forEach(element => {
        //   debugger
        //   if (element.controls['row'].controls['id'].value == itemRowLabel.id) {
        //     let ind = element.index;
        //     return ind;
        //   }
        // });
        // if (needcontrol.controls['row'].controls['id'].value == ) {

        // }
        if (!!needcontrol) {
            answerControl.removeAt(item2);
        }

        // let index = answerControl.controls.findIndex(x => x.id == itemRowLabel.id)
        // // let index = group.controls.findIndex(x => x.id == itemRowLabel.id)
        // answerControl.removeAt(this.lastSelectRowId);

        const formGroup = new FormGroup(group);
        answerControl.push(formGroup);
        this.lastSelectRowId = formGroup;

        //   debugger
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
}
