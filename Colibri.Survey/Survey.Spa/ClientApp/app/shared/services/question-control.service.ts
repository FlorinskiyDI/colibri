import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { QuestionBase } from '../models/form-builder/question-base.model';
import { ControTypes } from '../constants/control-types.constant';

// helper
import { GUID } from '../helpers/guide-type.helper';

import { TextboxQuestion } from '../Models/form-builder/question-textbox.model';
// import { DropdownQuestion } from '../Models/form-builder/question-dropdown.model';
// import { TextAreaQuestion } from '../Models/form-builder/question-textarea.model';
// import { RadioQuestion } from '../Models/form-builder/question-radio.model';
// import { CheckboxQuestion } from '../Models/form-builder/question-checkbox.model';
// import { GridRadioQuestion } from '../Models/form-builder/question-grid-radio.model';

@Injectable()
export class QuestionControlService {

  constructor(private fb: FormBuilder) { }

  questionsLength: number;

  toFormGroup(questions: QuestionBase<any>[]) {
    this.questionsLength = Object.keys(questions).length;
    const group: any = {};

    questions.forEach((question: any) => {
      if (question.controlType === ControTypes.checkbox) {
        group[question.key] = this.fb.group({
          'answer': question.required ? this.fb.array([], Validators.required) : this.fb.array([]),
          'additionalAnswer': new FormControl('')
        });
      }
      if (question.controlType === ControTypes.gridRadio) {
        group[question.key] = this.fb.group({
          'answer': this.fb.group({
            'rows': this.fb.group({}),
            'cols': this.fb.group({})
          }),
          'additionalAnswer': new FormControl('')
        });
      } else {

        group[question.key] = this.fb.group({
          'answer': new FormControl(question.value || ''),
          'additionalAnswer': new FormControl('')
        });

      }

    });
    return new FormGroup(group);
  }




  addTextboxControl(): QuestionBase<any> {
    const key = GUID.getNewGUIDString(); // new guid
    const question = new TextboxQuestion({
      key: key,
      label: 'default question text for type control "textbox"',
      type: 'email',
      order: this.questionsLength + 1,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }



}
