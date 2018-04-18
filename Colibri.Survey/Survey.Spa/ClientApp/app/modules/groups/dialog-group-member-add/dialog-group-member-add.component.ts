import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* component */ import { FormGroupMemberAddComponent } from './form-group-member-add/form-group-member-add.component';
/* component-config */ import { FormGroupMemberAddConfig } from './form-group-member-add/form-group-member-add.component';

@Component({
    selector: 'dialog-group-member-add-cmp',
    templateUrl: './dialog-group-member-add.component.html'
})

export class DialogGroupMemberAddComponent {
    dialogConfig: DialogDataModel<any> = new DialogDataModel();
    @Output() onChange = new EventEmitter<any>();
    @Output() onCancel = new EventEmitter<any>();
    @Output() onHide = new EventEmitter<any>();
    @Input()
    get config() { return this.dialogConfig; }
    set config(data: DialogDataModel<any>) {
        if (data) {
            this.dialogConfig = data;
            if (data.extraData) { this._cmpInitialize(data.extraData); }
        }
    }

    @ViewChild('ctrlFormGroupMemberAdd') ctrlFormGroupMemberAdd: FormGroupMemberAddComponent;
    formGroupMemberAddConfig: FormGroupMemberAddConfig;
    constructor() { }

    ngOnInit() { }
    ngOnDestroy() {
        this.onCancel.unsubscribe();
        this.onChange.unsubscribe();
        this.onHide.unsubscribe();
    }

    public formGroupMemberAddOnChange(data: any) {
        this.dialogChange(data);
    }
    public formSave() {
        this.ctrlFormGroupMemberAdd.formSubmit();
    }

    // #region Dialog action handling
    public dialogCancel() {
        this.dialogConfig.visible = false;
        this.onCancel.emit();
    }
    public dialogChange(data: any | null = null) {
        this.dialogConfig.visible = false;
        this.onChange.emit(data);
    }
    public dialogHide() {
        this.ctrlFormGroupMemberAdd.formReset();
        this.onHide.emit();
        this._cmpClear();
    }
    // #endregion

    private _cmpInitialize(data: any) {
        if (data) {
            this.formGroupMemberAddConfig = new FormGroupMemberAddConfig(data);
        }
    }
    private _cmpClear() { }
}
