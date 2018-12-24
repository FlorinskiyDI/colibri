import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* helper */ import { FormGroupHelper } from 'shared/helpers/form-group.helper';
/* model-api */ import { SearchQueryApiModel } from 'shared/models/entities/api/page-search-entry.api.model';

@Component({
    selector: 'group-dialog-create-cmp',
    templateUrl: './group-dialog-create.component.html',
    styleUrls: ['./group-dialog-create.component.scss'],
    providers: [ MessageService ]
})

export class GroupDialogCreateComponent {
    // dialog variable
    dialogConfig: DialogDataModel<any> = new DialogDataModel();
    @Output() onChange = new EventEmitter<any>();
    @Output() onCancel = new EventEmitter<any>();
    @Output() onHide = new EventEmitter<any>();
    @Input()
    get config() { return this.dialogConfig; }
    set config(data: DialogDataModel<any>) {
        if (data) {
            this.dialogConfig = data;
            this.componentInit();
        }
    }
    // form variable
    @ViewChild('ngFormGroup') ngFormGroup: any;
    formGroup: FormGroup;
    drpdwnGroups: any[] = [];

    treeSelected: any;
    treeItems: any[] = [];
    treeloading = false;
    isSubGroup = false;

    constructor(
        private messageService: MessageService,
        private groupsApiService: GroupsApiService
    ) {
    }

    ngOnInit() { this.formInitBuild(); }
    ngOnDestroy() { this.componentClear(); }
    private componentInit() {
        this.isSubGroup = false;
        this.treeItems = [];
        this._getAllRoot();
    }
    private componentClear() {
        this.formInitBuild();
        this.drpdwnGroups = [];
    }

    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'name': new FormControl(data.name, [Validators.required]),
            'groupID': new FormControl(null, [Validators.required]),
            'isSubgroup': new FormControl(null, [Validators.required]),
            'parentId': new FormControl(data.parentId),
            'description': new FormControl(data.description),
        });
    }

    public formSubmit() {
        if (!this.ngFormGroup.valid) {
            FormGroupHelper.setFormControlsAsTouched(this.formGroup);
            return;
        }
        this.groupsApiService
            .create(Object.assign({}, this.ngFormGroup.value))
            .subscribe(
                (response: any) => {
                    this.dialogChange();
                    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was created successfully' });
                },
                (error: any) => { }
            );
    }
    public formCancel() { this.dialogCancel(); }

    _getAllRoot() {
        this.treeloading = true;
        const that = this;
        this.groupsApiService.getAllRoot(null).subscribe((data: any) => {
            this.treeItems = data.itemList.map((item: any) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id },
                    'leaf': false
                };
            });
            that.treeloading = false;
        });
    }

    onChckboxChange(data: any) {
        console.log(data);
        console.log(this.isSubGroup);
        if (this.isSubGroup) {
            this._getAllRoot();
            this.formGroup.controls['parentId'].setValidators([Validators.required]);
            this.formGroup.controls['parentId'].markAsUntouched();
            this.formGroup.controls['parentId'].updateValueAndValidity();
        } else {
            this.formGroup.controls['parentId'].setValue(null);
            this.formGroup.controls['parentId'].clearValidators();
            this.formGroup.controls['parentId'].updateValueAndValidity();
            this.treeItems = [];
        }
    }

    onNodeSelect(event: any) {
        this.formGroup.controls['parentId'].setValue(event.node.data.id);
    }

    onNodeExpand(event: any) {
        this.treeloading = true;
        const that = this;
        this.groupsApiService.getSubgroups(new SearchQueryApiModel(), event.node.data.id).subscribe((data: Array<GroupApiModel>) => {
            event.node.children = data.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id },
                    'leaf': false
                };
            });
            that.treeloading = false;
        });
    }


    //#region Dialog
    public dialogCancel() {
        this.dialogConfig.visible = false;
        this.onCancel.emit();
    }
    public dialogChange(data: any | null = null) {
        this.dialogConfig.visible = false;
        this.onChange.emit(data);
    }
    public dialogHide() {
        this.onHide.emit();
        this.componentClear();
    }
    //#endregion end dialog

}
