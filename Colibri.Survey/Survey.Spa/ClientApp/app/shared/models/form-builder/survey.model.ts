
import { PageModel } from './page.model';

export class SurveyModel {

    id: string;
    name: string;
    description: string;
    pages: PageModel[];
    constructor(options: any = {}) {
        this.id = options['id'];
        this.name = options['name'] || '';
        this.description = options['description'] || '';
        this.pages = options['pages'];
    }
}
