import { Injectable } from '@angular/core';


import { QuestionBase } from '../../Models/form-builder/question-base.model';

// import { TextboxQuestion } from '../../Models/form-builder/question-textbox.model';
// import { DropdownQuestion } from '../../Models/form-builder/question-dropdown.model';
// import { TextAreaQuestion } from '../../Models/form-builder/question-textarea.model';
import { RadioQuestion } from '../../Models/form-builder/question-radio.model';
import { CheckboxQuestion } from '../../Models/form-builder/question-checkbox.model';
// import { GridRadioQuestion } from '../../Models/form-builder/question-grid-radio.model';
@Injectable()
export class QuestionService {

  // Todo: get from a remote source of question metadata
  // Todo: make asynchronous
  getQuestions() {

    const questions: QuestionBase<any>[] = [

      // new TextboxQuestion({
      // //  controlType: 'textbox', // same as in control-type.constant.ts
      //   id: 'id_firstName',
      //   text: 'First name',
      //   description: 'some description',
      //   value: '',
      //   required: true,
      //   order: 0,
      //   isAdditionalAnswer: true
      // }),

      // new DropdownQuestion({
      //   // controlType: 'dropdown', // same as in control-type.constant.ts
      //   id: 'id_brave', // unique value
      //   text: 'Bravery Rating (text)',
      //   description: 'some description',
      //   options: [
      //     { id: 'id_solid', label: '', value: 'Solid', order: 1 },
      //     { id: 'id_great', label: '', value: 'Great', order: 2 },
      //     { id: 'id_good', label: '', value: 'Good', order: 3 },
      //     { id: 'id_unproven', label: '', value: 'Unproven', order: 4 }
      //   ],
      //   order: 0,
      //   required: false,
      //   isAdditionalAnswer: false
      // }),

      // new TextAreaQuestion({
      // //  controlType: 'textarea', // same as in control-type.constant.ts
      //   id: 'id_lastname',
      //   text: 'last name',
      //   description: 'some description',
      //   value: '',
      //   required: false,
      //   order: 1,
      //   isAdditionalAnswer: false
      // }),


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
        required: false,
        isAdditionalAnswer: false
      }),

      new CheckboxQuestion({
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

      // new GridRadioQuestion({
      //   id: '10b08afca4dff80e975f4910ee85ef3f',
      //   key: 'grid radio',
      //   label: 'grid question',
      //   description: 'some description',
      //   grid: {
      //     cellInputType: 'radio',  // radio, checkbox
      //     rows: [
      //       {
      //         id: '48b09d72e6fb0d2a63985eef4018346e',
      //         orderNo: 1,
      //         label: 'row 1'
      //       },
      //       {
      //         id: 'f35a6e5d1ce9407b5ece224198032cb6',
      //         orderNo: 2,
      //         label: 'row 2'
      //       },
      //       {
      //         id: 'f35a6e5d1ce9407b5ece224198032cb8',
      //         orderNo: 3,
      //         label: 'row 3'
      //       },
      //       {
      //         id: 'f35a6e5d1ce9407b5ece224198032cb9',
      //         orderNo: 4,
      //         label: 'row 4'
      //       }
      //     ],
      //     cols: [
      //       {
      //         id: 'ace63d4001112c28e97b00ff67ceeeca',
      //         orderNo: 1,
      //         label: 'col 1'
      //       },
      //       {
      //         id: '24062ae1fc97dead41d337ede7f2e55e',
      //         orderNo: 2,
      //         label: 'col2'
      //       },
      //       {
      //         id: '24062ae1fc97dead41d337ede7f2e35e',
      //         orderNo: 3,
      //         label: 'col3'
      //       },
      //       {
      //         id: '24062ae1fc97dead41d337ede7f2e45e',
      //         orderNo: 4,
      //         label: 'col4'
      //       },
      //       {
      //         id: '24062ae1fc97dead41d337ede7f2e54e',
      //         orderNo: 5,
      //         label: 'col5'
      //       },
      //     ]
      //   },
      //   pageFlowModifier: false,
      //   order: 2,
      //   required: false,
      //   isAdditionalAnswer: true
      // })
    ];

    return questions.sort((a, b) => a.order - b.order);
  }
}
