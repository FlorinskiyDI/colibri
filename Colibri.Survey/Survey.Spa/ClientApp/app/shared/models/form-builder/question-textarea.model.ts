import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class TextAreaQuestion extends QuestionBase<string> {
  controlType = ControTypes.textarea;
  type: string;

  constructor(options: any = {}) {
    super(options);
    this.type = options['type'] || '';
  }
}
