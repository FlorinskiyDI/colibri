export class SearchEntryApiModel {
    public pageLength = 10;
    public filterStatements: Array<PageFilterStatement> = [];
    public orderStatement: PageOrderStatement;
    public globalSearch: string;
}
export class PageSearchEntryApiModel extends SearchEntryApiModel {
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

