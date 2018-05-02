import { Injectable } from '@angular/core';
import { FormControl, FormBuilder, Validators } from '@angular/forms';

// import { ControlOptionModel } from '../models/form-builder/form-control/control-option.model';
// import { QuestionBase } from '../models/form-builder/question-base.model';
// import { ControTypes } from '../constants/control-types.constant';

// helper
import { GUID } from '../helpers/guide-type.helper';

// import { TextboxQuestion } from '../Models/form-builder/question-textbox.model';
// import { DropdownQuestion } from '../Models/form-builder/question-dropdown.model';
// import { TextAreaQuestion } from '../Models/form-builder/question-textarea.model';
// import { RadioQuestion } from '../Models/form-builder/question-radio.model';
// import { CheckboxQuestion } from '../Models/form-builder/question-checkbox.model';
// import { GridRadioQuestion } from '../Models/form-builder/question-grid-radio.model';

@Injectable()
export class AnswerControlService {



  constructor(private fb: FormBuilder) { }


  addItemAnswer(group: any, required: boolean): any {
    return group[GUID.getNewGUIDString()] = this.fb.group({
      'label': !required ? new FormControl('') : new FormControl('', Validators.required)
    });
  }

}
