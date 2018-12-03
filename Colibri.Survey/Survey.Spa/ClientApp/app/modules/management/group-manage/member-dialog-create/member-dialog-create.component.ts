import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';

/* helper */ import { CHECK_EMAIL } from 'shared/helpers/check-email.helper';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';

@Component({
    selector: 'cmp-member-dialog-create',
    templateUrl: './member-dialog-create.component.html',
    styleUrls: ['./member-dialog-create.component.scss'],
    providers: [MessageService]
})

export class MemberDialogCreateComponent {
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
    formIsValid = true;
    formIsValidEmail = true;
    configData: any;

    constructor(
        private groupsApiService: GroupsApiService
    ) {
    }

    ngOnInit() {
        this.formInitBuild();
    }
    ngOnDestroy() { this.componentClear(); }
    private componentInit() {
    }
    private componentClear() {
        this.formInitBuild();
    }

    chipsOnAdd(event: any) {
        this.formIsValidEmail = true;
        if (!CHECK_EMAIL.validateEmail(event.value)) {
            this.formIsValidEmail = false;
            const index = this.formGroup.controls.members.value.findIndex((x: any) => x === event.value);
            this.formGroup.controls.members.value.splice(index, 1);
            console.log(this.formGroup.controls.members.value);
        }
    }
    chipsOnRemove(event: any) {
        this.formIsValidEmail = true;
    }


    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'members': new FormControl(data.name, [Validators.required])
        });
    }
    public formSubmit() {
        if (!this.formGroup.valid) {
            this.formIsValid = false;
            console.log('(FormGroupMemberAddComponent) Form is NOT VALID!!!');
            return;
        }

        const items: string[] = Object.assign([], this.formGroup.value.members);
        this.groupsApiService.addMembers(this.configData.groupId, items).subscribe((response: any) => {
            this.onChange.emit();
            this.componentClear();
        });
    }
    public formReset() {
        this.componentClear();
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
