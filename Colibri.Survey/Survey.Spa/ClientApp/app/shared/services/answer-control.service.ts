import { Injectable } from '@angular/core';
import { FormControl, FormBuilder, Validators } from '@angular/forms';

// import { ControlOptionModel } from '../models/form-builder/form-control/control-option.model';
// import { QuestionBase } from '../models/form-builder/question-base.model';
// import { ControTypes } from '../constants/control-types.constant';

// helper
import { GUID } from '../helpers/guide-type.helper';

// import { TextboxQuestion } from '../models\form-builder/question-textbox.model';
// import { DropdownQuestion } from '../models\form-builder/question-dropdown.model';
// import { TextAreaQuestion } from '../models\form-builder/question-textarea.model';
// import { RadioQuestion } from '../models\form-builder/question-radio.model';
// import { CheckboxQuestion } from '../models\form-builder/question-checkbox.model';
// import { GridRadioQuestion } from '../models\form-builder/question-grid-radio.model';

@Injectable()
export class AnswerControlService {



  constructor(private fb: FormBuilder) { }


  addItemAnswer(group: any, required: boolean): any {
    return group[GUID.getNewGUIDString()] = this.fb.group({
      'label': !required ? new FormControl('') : new FormControl('', Validators.required)
    });
  }

}
