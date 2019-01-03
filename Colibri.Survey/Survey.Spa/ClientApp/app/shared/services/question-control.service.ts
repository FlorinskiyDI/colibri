import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators, FormBuilder } from '@angular/forms';

import { ControlOptionModel } from '../models/form-builder/form-control/control-option.model';
import { QuestionBase } from '../models/form-builder/question-base.model';
import { ControTypes } from '../constants/control-types.constant';

// helpers
import { GUID } from '../helpers/guide-type.helper';

import { TextboxQuestion } from '../models/form-builder/question-textbox.model';
import { DropdownQuestion } from '../models/form-builder/question-dropdown.model';
import { TextAreaQuestion } from '../models/form-builder/question-textarea.model';
import { RadioQuestion } from '../models/form-builder/question-radio.model';
import { CheckboxQuestion } from '../models/form-builder/question-checkbox.model';
import { GridRadioQuestion } from '../models/form-builder/question-grid-radio.model';

@Injectable()
export class QuestionControlService {



  constructor(private fb: FormBuilder) { }
  group: any = {};
  questionsLength: number;
  // questionList: any[];

  toFormGroup(questions: QuestionBase<any>[]) {
    this.group = {};
    // this.questionList = questions;

    if (questions) {
      questions.forEach((question: any) => {

        this.addTypeAnswer(question, this.group);
      });
    }




    return new FormGroup(this.group);
  }



  addTypeAnswer(question: QuestionBase<any>, group: any): any {

    switch (question.controlType) {
      case ControTypes.checkbox: {

        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'answer': !question.required ? this.fb.array([]) : this.fb.array([], Validators.required),
          'additionalAnswer': new FormControl(''),
          'options': this.buildOptions(question.options)
        });
        break;
      }
      case ControTypes.gridRadio: {

        // group[question.id] = this.fb.group({
        //   'type': new FormControl(question.controlType),
        //   'answer': question.required ? this.fb.array([]) : this.fb.array([], Validators.required),
        //   'additionalAnswer': new FormControl('')
        // });


        const groupGrid: any = {};
        question.grid.rows.forEach((item: any) => {
          groupGrid[item.id] = this.fb.group({
            'label': !question.required ? new FormControl(item.label || '') : new FormControl(item.label || '', Validators.required),
          });
        });

        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'rows': this.fb.group(groupGrid),
          'answer': this.fb.array([]),
          'additionalAnswer': new FormControl('')
        });

        break;
      }

      case ControTypes.checkbox: {
        debugger
        group[question.id] = this.fb.group({
          'type': new FormControl(question.controlType),
          'answer': !question.required ? new FormControl(question.value || '') : new FormControl(question.value || '', Validators.required),
          'additionalAnswer': new FormControl(''),
          'options': this.buildOptions(question.options)
        });


        break;
      }




      default: {
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




  buildOptions(options: any) {
    const arr = options.map((opt: any) => {
      return this.fb.control(false);
    });
    return this.fb.array(arr);
  }


  addTextboxControl(index: number): QuestionBase<any> {
    const id = GUID.getNewGUIDString(); // new guid
    const question = new TextboxQuestion({
      value: '',
      id: id,
      text: 'Simple text ...',
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
      value: '',
      id: id,
      text: 'Extended text ...',
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
      text: 'Single choice ...',
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
      text: 'Multiple choice ...',
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
      text: 'Select variable ...',
      description: 'some description!',
      options: [
        new ControlOptionModel(GUID.getNewGUIDString(), '1111', 'dropdown value 1', 0),
        new ControlOptionModel(GUID.getNewGUIDString(), '2222', 'dropdown value 2', 1)
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
      text: 'Table with single row`s choice ...',
      description: 'Some description ...',
      grid: {
        cellInputType: 'radio',  // radio, checkbox
        rows: [
          { id: 'id_question1', label: null, value: 'Variable question 1', order: 1 },
          { id: 'id_question2', label: null, value: 'Variable question 2', order: 2 }
        ],
        cols: [
          { id: 'id_answer1', label: null, value: 'answer 1', order: 1 },
          { id: 'id_answer2', label: null, value: 'answer 2', order: 2 },
        ]
      },
      pageFlowModifier: false,
      order: index,
      required: false,
      isAdditionalAnswer: false
    });
    return question;
  }

}
