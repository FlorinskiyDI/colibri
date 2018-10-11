
import { QuestionBase } from './question-base.model';
import { ControStates } from '../../constants/control-states.constant';

export class PageModel {


    id: string;
    name: string;
    description: string;
    order: number;
    questions: QuestionBase<any>[];
    isChanged?: boolean;
    state: any;
    constructor(options: any = {}) {

        this.id = options['id'];
        this.name = options['name'] || '';
        this.description = options['description'] || '';
        this.order = options['order'] === undefined ? 1 : options['order'];
        this.questions = options['questions'];
        this.isChanged = false;
        this.state = options['state'] === undefined ? ControStates.unchanged : ControStates.created;

    }
}
