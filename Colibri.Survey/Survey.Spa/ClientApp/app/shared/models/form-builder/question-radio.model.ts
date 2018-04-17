import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class RadioQuestion extends QuestionBase<string> {
  controlType = ControTypes.radio;
  options: {key: string, value: string}[] = [];

  constructor(options: any = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
