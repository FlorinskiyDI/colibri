
import { QuestionBase } from './question-base.model';

export class PageModel {


    id: string;
    name: string;
    description: string;
    order: number;
    questions: QuestionBase<any>[];

    constructor(options: any = {}) {

        this.id = options['id'];
        this.name = options['name'] || '';
        this.description = options['description'] || '';
        this.order = options['order'] === undefined ? 1 : options['order'];
        this.questions = options['questions'];

    }
}
