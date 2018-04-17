import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class CheckboxQuestion extends QuestionBase<string> {
  controlType = ControTypes.checkbox;
  options: {key: string, value: boolean, label: string}[] = [];

  constructor(options: any = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
