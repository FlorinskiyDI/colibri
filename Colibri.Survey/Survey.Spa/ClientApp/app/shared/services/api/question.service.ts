import { Injectable } from '@angular/core';


import { QuestionBase } from '../../Models/form-builder/question-base.model';

import { TextboxQuestion } from '../../Models/form-builder/question-textbox.model';
import { DropdownQuestion } from '../../Models/form-builder/question-dropdown.model';
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
      //  controlType: 'textbox', // same as in control-type.constant.ts
        id: 'id_firstName',
        text: 'First name',
        description: 'some description',
        value: '',
        required: true,
        order: 0,
        isAdditionalAnswer: true
      }),

      new DropdownQuestion({
        // controlType: 'dropdown', // same as in control-type.constant.ts
        id: 'id_brave', // unique value
        text: 'Bravery Rating (text)',
        description: 'some description',
        options: [
          { id: 'id_solid', label: '', value: 'Solid', order: 1 },
          { id: 'id_great', label: '', value: 'Great', order: 2 },
          { id: 'id_good', label: '', value: 'Good', order: 3 },
          { id: 'id_unproven', label: '', value: 'Unproven', order: 4 }
        ],
        order: 0,
        required: true,
        isAdditionalAnswer: true
      }),

      new TextAreaQuestion({
      //  controlType: 'textarea', // same as in control-type.constant.ts
        id: 'id_lastname',
        text: 'last name',
        description: 'some description',
        value: '',
        required: true,
        order: 1,
        isAdditionalAnswer: true
      }),


      new RadioQuestion({
        //  controlType: 'radio', // same as in control-type.constant.ts
        id: 'id_Radion question',
        text: 'Bravery radion',
        description: 'some description',
        options: [
          { id: 'id_Option 1', label: false, value: 'Option 1', order: 1 },
          { id: 'id_Option 2', label: false, value: 'Option 2', order: 2 },
          { id: 'id_Option 3', label: false, value: 'Option 3', order: 3 },
          { id: 'id_Option 4', label: false, value: 'Option 4', order: 4 },
        ],
        order: 7,
        required: true,
        isAdditionalAnswer: true
      }),

      new CheckboxQuestion({
        //  controlType: 'checkbox', // same as in control-type.constant.ts
        id: 'id_Checkbox groupe',
        text: 'Bravery checkbox',
        description: 'some description',
        options: [
          { id: 'id_Option 1', label: false, value: 'Variable 1', order: 1 },
          { id: 'id_Option 2', label: false, value: 'Variable 2', order: 2 },
          { id: 'id_Option 3', label: false, value: 'Variable 3', order: 3 },
        ],
        order: 0,
        required: true,
        isAdditionalAnswer: true
      }),

      new GridRadioQuestion({
        //  controlType: 'gridRadio', // same as in control-type.constant.ts
        id: 'id_grid radio',
        text: 'Grid question, some text for long input, some text for long input, some text for long input, some text for long input',
        description: 'Some description ...',
        grid: {
          cellInputType: 'radio',  // radio, checkbox
          rows: [
            { id: 'id_question1', label: null, value: 'Variable question 1', order: 1 },
            { id: 'id_question2', label: null, value: 'Variable question 2', order: 2 },
            { id: 'id_question3', label: null, value: 'Variable question 3', order: 3 },
            { id: 'id_question4', label: null, value: 'Variable question 4', order: 4 }
            // {
            //   id: 'f35a6e5d1ce9407b5ece224198032cb6',
            //   orderNo: 2,
            //   label: 'row 2'
            // },
          ],
          cols: [
            { id: 'id_answer1', label: null, value: 'answer 1', order: 1 },
            { id: 'id_answer2', label: null, value: 'answer 2', order: 2 },
            { id: 'id_answer3', label: null, value: 'answer 3', order: 3 },
            { id: 'id_answer4', label: null, value: 'answer 4', order: 4 },
            { id: 'id_answer5', label: null, value: 'answer 5', order: 5 }
            // {
            //   id: '24062ae1fc97dead41d337ede7f2e54e',
            //   orderNo: 5,
            //   label: 'col5'
            // },
          ]
        },
        pageFlowModifier: false,
        order: 2,
        required: true,
        isAdditionalAnswer: true
      })
    ];

    return questions.sort((a, b) => a.order - b.order);
  }
}
