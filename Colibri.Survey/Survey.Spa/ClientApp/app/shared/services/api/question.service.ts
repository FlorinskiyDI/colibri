import { Injectable } from '@angular/core';

import { TextboxQuestion } from '../../Models/form-builder/question-textbox.model';
import { DropdownQuestion } from '../../Models/form-builder/question-dropdown.model';
import { QuestionBase } from '../../Models/form-builder/question-base.model';
import { TextAreaQuestion } from '../../Models/form-builder/question-textarea.model';
import { RadioQuestion } from '../../Models/form-builder/question-radio.model';
import { CheckboxQuestion } from '../../Models/form-builder/question-checkbox.model';
import { GridRadioQuestion } from '../../Models/form-builder/question-grid-radio.model';
@Injectable()
export class QuestionService {

  // Todo: get from a remote source of question metadata
  // Todo: make asynchronous
  getQuestions() {

    const questions: QuestionBase<any>[] = [

      new TextboxQuestion({
        key: 'firstName',
        label: 'First name',
        value: '',
        required: true,
        order: 0,
        isAdditionalAnswer: false
      }),

      new DropdownQuestion({
        key: 'brave',
        label: 'Bravery Rating',
        options: [
          { key: 'solid', value: 'Solid' },
          { key: 'great', value: 'Great' },
          { key: 'good', value: 'Good' },
          { key: 'unproven', value: 'Unproven' }
        ],
        order: 0,
        isAdditionalAnswer: false
      }),

      new TextAreaQuestion({
        key: 'lastname',
        label: 'last name',
        value: '',
        required: true,
        order: 1,
        isAdditionalAnswer: false
      }),

      new TextboxQuestion({
        key: 'emailAddress',
        label: 'Email',
        type: 'email',
        order: 2,
        isAdditionalAnswer: false
      }),

      new RadioQuestion({
        key: 'Radion question',
        label: 'Bravery radion',
        options: [
          { key: 'Option 1', value: 'opt_1' },
          { key: 'Option 2', value: 'opt_2' },
          { key: 'Option 3', value: 'opt_3' },
          { key: 'Option 4', value: 'opt_4' }
        ],
        order: 7,
        required: false,
      }),

      new CheckboxQuestion({
        key: 'Checkbox groupe',
        label: 'Bravery checkbox',
        options: [
          { key: 'opt_1', label: 'Option 1', value: false },
          { key: 'opt_2', label: 'Option 2', value: false },
          { key: 'opt_3', label: 'Option 3', value: false },
          { key: 'opt_4', label: 'Option 4', value: false },
          { key: 'opt_5', label: 'Option 5', value: false }
        ],
        order: 0,
        required: false,
        isAdditionalAnswer: false
      }),

      new GridRadioQuestion({
        id: '10b08afca4dff80e975f4910ee85ef3f',
        key: 'grid radio',
        label: 'grid question',
        type: 'grid',
        grid: {
          cellInputType: 'radio',  // radio, checkbox
          rows: [
            {
              id: '48b09d72e6fb0d2a63985eef4018346e',
              orderNo: 1,
              label: 'row 1'
            },
            {
              id: 'f35a6e5d1ce9407b5ece224198032cb6',
              orderNo: 2,
              label: 'row 2'
            },
            {
              id: 'f35a6e5d1ce9407b5ece224198032cb8',
              orderNo: 3,
              label: 'row 3'
            },
            {
              id: 'f35a6e5d1ce9407b5ece224198032cb9',
              orderNo: 4,
              label: 'row 4'
            }
          ],
          cols: [
            {
              id: 'ace63d4001112c28e97b00ff67ceeeca',
              orderNo: 1,
              label: 'col 1'
            },
            {
              id: '24062ae1fc97dead41d337ede7f2e55e',
              orderNo: 2,
              label: 'col2'
            },
            {
              id: '24062ae1fc97dead41d337ede7f2e35e',
              orderNo: 3,
              label: 'col3'
            },
            {
              id: '24062ae1fc97dead41d337ede7f2e45e',
              orderNo: 4,
              label: 'col4'
            },
            {
              id: '24062ae1fc97dead41d337ede7f2e54e',
              orderNo: 5,
              label: 'col5'
            },
          ]
        },
        pageFlowModifier: false,
        order: 2,
        required: false,
        isAdditionalAnswer: true
      })
    ];

    return questions.sort((a, b) => a.order - b.order);
  }
}
