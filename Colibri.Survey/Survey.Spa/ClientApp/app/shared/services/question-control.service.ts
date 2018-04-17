import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { QuestionBase } from '../models/form-builder/question-base.model';
import { ControTypes } from '../constants/control-types.constant';

@Injectable()
export class QuestionControlService {
  constructor(private fb: FormBuilder) { }

  toFormGroup(questions: QuestionBase<any>[]) {
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
}
