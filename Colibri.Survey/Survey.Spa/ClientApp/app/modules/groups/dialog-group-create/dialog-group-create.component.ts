import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* component-config */ import { FormGroupCreateConfig } from './form-group-create/form-group-create.component';
/* component */ import { FormGroupCreateComponent } from './form-group-create/form-group-create.component';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
// /* helper */ import { Helpers } from 'shared/helpers/helpers';


@Component({
    selector: 'dialog-group-create-cmp',
    templateUrl: './dialog-group-create.component.html',
    providers: [MessageService]
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
    blockedPanel = false;

    constructor(
        private messageService: MessageService,
        //
        private groupsApiService: GroupsApiService
    ) { }

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
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was created successfully' });
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
        this.blockedPanel = true;
        this.groupsApiService.getAll(['id', 'name'])
            .subscribe((response: Array<GroupApiModel>) => {
                this.formGroupConfig = new FormGroupCreateConfig(response);
                this.blockedPanel = false;
            });
    }
    private _cmpClear() { }
}
