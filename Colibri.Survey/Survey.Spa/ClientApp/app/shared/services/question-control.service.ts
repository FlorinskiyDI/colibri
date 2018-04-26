import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { ControlOptionModel } from '../models/form-builder/form-control/control-option.model';
import { QuestionBase } from '../models/form-builder/question-base.model';
import { ControTypes } from '../constants/control-types.constant';

// helper
import { GUID } from '../helpers/guide-type.helper';

import { TextboxQuestion } from '../Models/form-builder/question-textbox.model';
import { DropdownQuestion } from '../Models/form-builder/question-dropdown.model';
import { TextAreaQuestion } from '../Models/form-builder/question-textarea.model';
import { RadioQuestion } from '../Models/form-builder/question-radio.model';
import { CheckboxQuestion } from '../Models/form-builder/question-checkbox.model';
import { GridRadioQuestion } from '../Models/form-builder/question-grid-radio.model';

@Injectable()
export class QuestionControlService {



  constructor(private fb: FormBuilder) { }
  group: any = {};
  questionsLength: number;
  // questionList: any[];

  toFormGroup(questions: QuestionBase<any>[]) {
    // this.questionList = questions;

    questions.forEach((question: any) => {

      this.addTypeAnswer(question, this.group);
    });



    return new FormGroup(this.group);
  }



  addTypeAnswer(question: QuestionBase<any>, group: any): any {

    switch (question.controlType) {
      case ControTypes.checkbox: {
        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'answer': !question.required ? this.fb.array([]) : this.fb.array([], Validators.required),
          'additionalAnswer': new FormControl('')
        });
        break;
      }
      case ControTypes.gridRadio: {
        // debugger
        // group[question.id] = this.fb.group({
        //   'type': new FormControl(question.controlType),
        //   'answer': question.required ? this.fb.array([]) : this.fb.array([], Validators.required),
        //   'additionalAnswer': new FormControl('')
        // });


        const groupGrid: any = {};
        question.grid.rows.forEach((item: any) => {
          groupGrid[item.id] = this.fb.group({
            'label': new FormControl('', Validators.required)
          });
        });

        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'rows': this.fb.group(groupGrid),
          'answer': !question.required ? this.fb.array([]) : this.fb.array([], Validators.required),
          'additionalAnswer': new FormControl('')
        });
        debugger
        break;
      }






      default: {
        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'answer': !question.required ? new FormControl(question.value || '') : new FormControl(question.value || '', Validators.required),
          'additionalAnswer': new FormControl('')
        });







        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'answer': !question.required ? new FormControl(question.value || '') : new FormControl(question.value || '', Validators.required),
          'additionalAnswer': new FormControl('')
        });
        break;
      }
    }

    return group[question.id];
  }


  addTextboxControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid
    const question = new TextboxQuestion({
      id: id,
      text: 'default question text for type control "textbox"',
      description: 'some description!',
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }


  addTextareaControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid
    const question = new TextAreaQuestion({
      id: id,
      text: 'default question text for type control "textarea"',
      description: 'some description!',
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }

  addRadioButtonControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid
    const question = new RadioQuestion({
      id: id,
      text: 'new "radiobutton"',
      description: 'some description!',
      options: [
        new ControlOptionModel(GUID.getNewGUIDString(), '', 'radio 1', 0),
        new ControlOptionModel(GUID.getNewGUIDString(), '', 'radio 2', 1),
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });

    return question;
  }



  addCheckBoxControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid


    const question = new CheckboxQuestion({
      id: id,
      text: 'Bravery checkbox',
      description: 'some description!',
      options: [
        new ControlOptionModel(GUID.getNewGUIDString(), false, 'variable 1', 0),
        new ControlOptionModel(GUID.getNewGUIDString(), false, 'variable 2', 1),
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }


  addDropdownControl(index: number): QuestionBase<any> {

    const id = GUID.getNewGUIDString(); // new guid

    const question = new DropdownQuestion({
      id: id,
      text: 'default question text for type control "dropdown"',
      description: 'some description!',
      options: [
        new ControlOptionModel(GUID.getNewGUIDString(), '', 'dropdown value 1', 0),
        new ControlOptionModel(GUID.getNewGUIDString(), '', 'dropdown value 2', 1)
      ],
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }


  addGridRadioControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid


    const question = new GridRadioQuestion({
      id: id,
      label: 'grid question',
      description: 'some description!',
      grid: {
        cellInputType: 'radio',  // radio, checkbox
        rows: [
          {
            id: '48b09d72e6fb0d2a63985eef4018346e',
            orderNo: 1,
            label: 'row 1'
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
          }
        ]
      },
      pageFlowModifier: false,
      order: index,
      required: false,
      isAdditionalAnswer: true
    });
    return question;
  }

}
