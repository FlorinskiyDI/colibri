import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class GridRadioQuestion extends QuestionBase<string> {
    controlType = ControTypes.gridRadio;
      options: {key: string, value: string, label: string}[] = [];
      grid: any;

    constructor(options: any = {}) {
        super(options);
        this.options = options['options'] || [];
    }
}
