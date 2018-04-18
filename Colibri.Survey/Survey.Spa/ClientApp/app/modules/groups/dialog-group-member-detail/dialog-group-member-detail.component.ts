import { Component, Output, Input, EventEmitter } from '@angular/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'dialog-group-member-detail-cmp',
    templateUrl: './dialog-group-member-detail.component.html'
})

export class DialogGroupMemberDetailComponent {
    dialogConfig: DialogDataModel<any> = new DialogDataModel();
    @Output() onHide = new EventEmitter<any>();
    @Input()
    get config() { return this.dialogConfig; }
    set config(data: DialogDataModel<any>) {
        if (data) {
            this.dialogConfig = data;
            if (data.extraData) { this._cmpInitialize(data.extraData); }
        }
    }

    constructor() { }

    ngOnInit() { }
    ngOnDestroy() {
        this.onHide.unsubscribe();
    }

    // #region Dialog action handling    
    public dialogHide() {
        this.onHide.emit();
        this._cmpClear();
    }
    // #endregion

    private _cmpInitialize(data: any) {
        if (data) {
            console.log(data);
        }
    }
    private _cmpClear() { }
}
