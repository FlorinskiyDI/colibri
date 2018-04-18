export class DialogDataModel<T> {
    public visible: boolean;

    private _extraData: T | null;
    get extraData():  T | null {
        return this._extraData;
    }

    constructor(visible: boolean = false, extraData: T | null = null ) {
        this.visible = visible;
        this._extraData = extraData;
    }
}
