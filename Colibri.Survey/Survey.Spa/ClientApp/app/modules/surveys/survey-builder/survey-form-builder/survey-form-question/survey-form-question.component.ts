import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, FormArray } from '@angular/forms';

import { QuestionBase } from '../../../../../shared/models/form-builder/question-base.model';

@Component({
    selector: 'survey-form-question',
    templateUrl: './survey-form-question.component.html',
    styleUrls: ['./survey-form-question.component.scss'],
})
export class SurveyFormQuestionComponent {
    @Input() question: QuestionBase<any>;
    @Input() form: FormGroup;
    get isValid() {
        return this.form.controls[this.question.key].valid;
    }
    get isDirty() { return this.form.controls[this.question.key].dirty; }



    onChange(key: any, label: string, isChecked: boolean) {

        const answer: any = 'answer';
        const checkBoxArray = <FormArray>this.form.controls[key];
        const checkBoxControl: any = checkBoxArray.controls[answer];
        console.log(checkBoxArray);
        if (isChecked) {
            checkBoxControl.push(new FormControl(label));
        } else {
            const index = checkBoxControl.controls.findIndex((x: any) => x.value === label);
            checkBoxControl.removeAt(index);
        }
    }
}
