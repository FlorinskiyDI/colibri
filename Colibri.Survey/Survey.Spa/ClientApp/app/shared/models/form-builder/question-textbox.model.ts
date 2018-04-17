import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class TextboxQuestion extends QuestionBase<string> {
  controlType = ControTypes.textbox;
  type: string;

  constructor(options: any = {}) {
    super(options);
    this.type = options['type'] || '';
  }
}
