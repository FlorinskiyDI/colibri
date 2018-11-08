
export class PageSearchEntryApiModel {
    public pageNumber = 0;
    public pageLength = 10;
    public globalSearch: string;
    public filterStatements: Array<PageFilterStatement> = [];
}

export class PageFilterStatement {
    public propertyName: string;
    public value: string;
    // public operation: Operation; // temporary
}

export enum Operation {

}

