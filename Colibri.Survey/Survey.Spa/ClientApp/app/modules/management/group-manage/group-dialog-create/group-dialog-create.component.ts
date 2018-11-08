import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* helper */ import { FormGroupHelper } from 'shared/helpers/form-group.helper';

@Component({
    selector: 'group-dialog-create-cmp',
    templateUrl: './group-dialog-create.component.html',
    providers: [MessageService]
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


    constructor(
        private messageService: MessageService,
        private groupsApiService: GroupsApiService
    ) { }

    ngOnInit() { this.formInitBuild(); }
    ngOnDestroy() { this.componentClear(); }
    private componentInit() { this.formInitControlData(); }
    private componentClear() {
        this.formInitBuild();
        this.drpdwnGroups = [];
    }

    // Form
    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'name': new FormControl(data.name, [Validators.required]),
            'groupID': new FormControl(null, [Validators.required]),
            'parentId': new FormControl(data.parentId),
            'description': new FormControl(data.description),
        });
    }
    private formInitControlData() {
        this.groupsApiService.getAll(['id', 'name']).subscribe(
            (response: Array<GroupApiModel>) => {
                const groups = response && response.map((item: any) => { return { label: item.name, value: item.id }; });
                this.drpdwnGroups = this.drpdwnGroups.concat([{ label: 'none' }]).concat(groups);

            });
    }
    public formSubmit() {
        if (!this.ngFormGroup.valid) {
            FormGroupHelper.setFormControlsAsTouched(this.formGroup);
            return;
        }

        // const group = Object.assign({}, this.ngFormGroup.value);
        this.groupsApiService
            .create(Object.assign({}, this.ngFormGroup.value))
            .subscribe(
                (response: any) => {
                    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was created successfully' });
                },
                (error: any) => { }
            );
    }
    public formCancel() { this.dialogCancel(); }
    // end form


    // Dialog
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
    // end dialog

}
