import { QuestionBase } from './question-base.model';
import { ControTypes } from '../../constants/control-types.constant';

export class DropdownQuestion extends QuestionBase<string> {
  controlType = ControTypes.dropdown;
  options: {key: string, value: string}[] = [];

  constructor(options: any = {}) {
    super(options);
    this.options = options['options'] || [];
  }
}
