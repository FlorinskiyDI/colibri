
import { PageModel } from './page.model';

export class SurveyModel {

    id: string;
    name: string;
    description: string;
    pages: PageModel[];
    isShowDescription: boolean;
    isOpenAccess: boolean;
    isShowProcessCompletedText: boolean;
    isLocked: boolean;
    processCompletedText: string;
    constructor(options: any = {}) {
        this.id = options['id'];
        this.name = options['name'] || '';
        this.description = options['description'] || '';
        this.pages = options['pages'];
        this.isShowDescription = options['isShowDescription'] || true;
        this.isShowProcessCompletedText = options['isShowProcessCompletedText'] || true;
        this.processCompletedText = options['processCompletedText'] || '';
        this.isOpenAccess = options['isOpenAccess'] || true;
        this.isLocked = options['isLocked'] || true;
    }
}
