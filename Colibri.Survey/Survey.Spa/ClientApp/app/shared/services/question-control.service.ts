import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { QuestionBase } from '../models/form-builder/question-base.model';
import { ControTypes } from '../constants/control-types.constant';

// helper
import { GUID } from '../helpers/guide-type.helper';

import { TextboxQuestion } from '../Models/form-builder/question-textbox.model';
import { DropdownQuestion } from '../Models/form-builder/question-dropdown.model';
import { TextAreaQuestion } from '../Models/form-builder/question-textarea.model';
import { RadioQuestion } from '../Models/form-builder/question-radio.model';
import { CheckboxQuestion } from '../Models/form-builder/question-checkbox.model';
// import { GridRadioQuestion } from '../Models/form-builder/question-grid-radio.model';

@Injectable()
export class QuestionControlService {

  constructor(private fb: FormBuilder) { }

  questionsLength: number;
  // questionList: any[];

  toFormGroup(questions: QuestionBase<any>[]) {
    // this.questionList = questions;

    const group: any = {};






    questions.forEach((question: any) => {
      switch (question.controlType) {
        case ControTypes.checkbox: {
          group[question.key] = this.fb.group({
            'answer': question.required ? this.fb.array([], Validators.required) : this.fb.array([]),
            'additionalAnswer': new FormControl('')
          });
          break;
        }
        case ControTypes.gridRadio: {
          group[question.key] = this.fb.group({
            'answer': question.required ? this.fb.array([], Validators.required) : this.fb.array([]),
            'additionalAnswer': new FormControl('')
          });
          break;
        }
        default: {
          group[question.key] = this.fb.group({
            'answer': new FormControl(question.value || ''),
            'additionalAnswer': new FormControl('')
          });
          break;
        }
      }

    });
    return new FormGroup(group);
  }




  addTextboxControl(index: number): QuestionBase<any> {
    const key = GUID.getNewGUIDString(); // new guid
    const question = new TextboxQuestion({
      key: key,
      label: 'default question text for type control "textbox"',
      type: 'email',
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }


  addTextareaControl(index: number): QuestionBase<any> {
    const key = GUID.getNewGUIDString(); // new guid
    const question = new TextAreaQuestion({
      key: key,
      label: 'default question text for type control "textarea"',
      type: 'textarea',
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }

  addRadioButtonControl(index: number): QuestionBase<any> {
    const key = GUID.getNewGUIDString(); // new guid
    const question = new RadioQuestion({
      key: key,
      label: 'default question text for type control "radiobutton"',
      options: [
        { key: 'Option 1', value: 'opt_1' },
        { key: 'Option 2', value: 'opt_2' },
        { key: 'Option 3', value: 'opt_3' },
        { key: 'Option 4', value: 'opt_4' }
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });

    return question;
  }



  addCheckBoxControl(index: number): QuestionBase<any> {
    const key = GUID.getNewGUIDString(); // new guid


    const question = new CheckboxQuestion({
      key: key,
      label: 'Bravery checkbox',
      options: [
        { key: 'opt_1', label: 'Option 1', value: false },
        { key: 'opt_2', label: 'Option 2', value: false },
        { key: 'opt_3', label: 'Option 3', value: false },
        { key: 'opt_4', label: 'Option 4', value: false },
        { key: 'opt_5', label: 'Option 5', value: false }
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }


  addDropdownControl(index: number): QuestionBase<any> {

    const key = GUID.getNewGUIDString(); // new guid

    const question = new DropdownQuestion({
      key: key,
      label: 'default question text for type control "dropdown"',
      options: [
        { key: 'solid', value: 'Solid' },
        { key: 'great', value: 'Great' },
        { key: 'good', value: 'Good' },
        { key: 'unproven', value: 'Unproven' }
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;

  }


}
