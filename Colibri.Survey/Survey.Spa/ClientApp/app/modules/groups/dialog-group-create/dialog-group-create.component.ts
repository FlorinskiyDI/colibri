import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* component-config */ import { FormGroupCreateConfig } from './form-group-create/form-group-create.component';
/* component */ import { FormGroupCreateComponent } from './form-group-create/form-group-create.component';

@Component({
    selector: 'dialog-group-create-cmp',
    templateUrl: './dialog-group-create.component.html'
})

export class DialogGroupCreateComponent {
    dialogConfig: DialogDataModel<any> = new DialogDataModel();
    @Output() onChange = new EventEmitter<any>();
    @Output() onCancel = new EventEmitter<any>();
    @Output() onHide = new EventEmitter<any>();
    @Input()
    get config() { return this.dialogConfig; }
    set config(data: DialogDataModel<any>) {
        if (data) {
            this.dialogConfig = data;
            this._cmpInitialize();
        }
    }

    @ViewChild('ctrlFormGroupCreate') ctrlFormGroupCreate: FormGroupCreateComponent;
    formGroupConfig: FormGroupCreateConfig;
    blockedPanel: boolean = false;

    constructor() { }

    ngOnInit() { }
    ngOnDestroy() {
        this.onCancel.unsubscribe();
        this.onChange.unsubscribe();
        this.onHide.unsubscribe();
    }

    public formGroupOnChange(data: any) {
        this.dialogChange(data);
    }
    public formSave() {
        this.ctrlFormGroupCreate.formSubmit();
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
        this.blockedPanel = false;
        this.ctrlFormGroupCreate.formReset();
        this.onHide.emit();
        this._cmpClear();
    }
    // #endregion

    private _cmpInitialize(data: any = null) {
        const that = this;
        this._stub1().then(function (value: any) {
            that.formGroupConfig = new FormGroupCreateConfig(value);
            that.blockedPanel = false;
        });

    }
    private _cmpClear() { }

    _stub1() {
        this.blockedPanel = true
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { name: 'Group 1', id: '111' },
                    { name: 'Group 2', id: '222' },
                ];
                resolve(data);
            }, 1500);
        });
    }
}
