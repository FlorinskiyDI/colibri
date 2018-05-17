import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';
import { ControlOptionModel } from './form-control/control-option.model';


export class RadioQuestion extends QuestionBase<string> {
  controlType = ControTypes.radio;
  options: ControlOptionModel[] = [];

  constructor(options: any = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
