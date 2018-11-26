export class SearchQueryApiModel {
    public searchQueryPage: SearchQueryPage;
    public filterStatements: Array<PageFilterStatement> = [];
    public orderStatement: PageOrderStatement;
    public globalSearch: string;
}
export class SearchQueryPage {
    public pageNumber = 0;
    public pageLength = 10;
}


export class PageOrderStatement {
    public columName: string;
    public reverse: boolean;
}

export class PageFilterStatement {
    public propertyName: string = null;
    public value: string = null;
    // public operation: Operation; // temporary
}

export enum Operation {

}

