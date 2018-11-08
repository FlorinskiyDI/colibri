
export class PageSearchEntryApiModel {
    public pageNumber = 0;
    public pageLength = 10;
    public filterStatements: Array<PageFilterStatement> = [];
    public orderStatement: PageOrderStatement;
    public globalSearch: string;
}

export class PageOrderStatement {
    public columName: string;
    public reverse: boolean;
}

export class PageFilterStatement {
    public propertyName: string;
    public value: string;
    // public operation: Operation; // temporary
}

export enum Operation {

}

