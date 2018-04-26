import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';
import { ControlOptionModel } from './form-control/control-option.model';

export class GridRadioQuestion extends QuestionBase<string> {
    controlType = ControTypes.gridRadio;
      options: ControlOptionModel[] = [];
      grid: any;

    constructor(options: any = {}) {
        super(options);
        this.options = options['options'] || [];
    }
}
