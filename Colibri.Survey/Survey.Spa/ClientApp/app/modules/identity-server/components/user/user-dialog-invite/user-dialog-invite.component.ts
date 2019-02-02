import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatChipInputEvent } from '@angular/material';
import { ErrorStateMatcher } from '@angular/material/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';
/* helper */ import { FormGroupHelper } from 'shared/helpers/form-group.helper';

@Component({
    selector: 'cmp-user-dialog-invite',
    templateUrl: './user-dialog-invite.component.html',
    styleUrls: ['./user-dialog-invite.component.scss'],
    providers: [MessageService]
})

export class UserDialogInviteComponent {
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
    matcher = new ErrorStateMatcher();
    @ViewChild('chipList') chipList: any;
    @ViewChild('ngFormGroup') ngFormGroup: any;
    formGroup: FormGroup;

    ////
    drpdwnRoles: Array<any> = [{ label: 'role 1', value: '1' }, { label: 'role 2', value: '2' }];
    visible = true;
    selectable = true;
    removable = true;
    addOnBlur = true;
    readonly separatorKeysCodes: number[] = [ENTER, COMMA];
    emailList: any[] = [];

    constructor(
        private fb: FormBuilder,
        private messageService: MessageService,
        private usersApiService: UsersApiService
    ) {
    }

    ngOnInit() { this.formInitBuild(); }
    ngOnDestroy() { this.componentClear(); }
    private componentInit() {
        // this.emailList = [];
        this._getRoles();
    }
    private componentClear() {
        this.formInitBuild();
    }

    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'emails': new FormControl(null, [Validators.required]),
            'roleArray': this.fb.array([]),
        });
        this.addRoleControl();
    }

    public formSubmit() {
        if (!this.ngFormGroup.valid) {
            console.log(this.ngFormGroup.value);
            console.log(this.formGroup);
            FormGroupHelper.setFormControlsAsTouched(this.formGroup);
            return;
        }

        const data = Object.assign(
            {},
            {
                emails: this.ngFormGroup.value.emails.map((item: any) => item.name),
                roles: this.ngFormGroup.value.roleArray
            });

        debugger
        this.usersApiService
            .setIamPolicy(data)
            .subscribe(
                (response: any) => {
                    this.dialogChange();
                    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was created successfully' });
                },
                (error: any) => { }
            );
    }
    public formCancel() { this.dialogCancel(); }

    _getRoles() {
        this.usersApiService
            .getIamPolicy()
            .subscribe(
                (response: any) => {
                    this.drpdwnRoles = [];
                    response.plain().forEach((item: any) => {
                        this.drpdwnRoles.push({ label: item, value: item });
                    });

                    // this.drpdwnRoles = response.plain();
                },
                (error: any) => { }
            );
    }

    addRoleControl() {
        const roleArray = this.formGroup.controls['roleArray'] as FormArray;
        // const control = this.fb.group({'role': [Validators.required] }) as FormGroup;
        roleArray.push(new FormControl(null, [Validators.required]));
        roleArray.updateValueAndValidity();
    }
    deleteRoleControl(item: any) {
        const roleArray = this.formGroup.get('roleArray') as FormArray;
        const index = roleArray.controls.indexOf(item);
        if (index !== -1) { roleArray.controls.splice(index, 1); }
        roleArray.updateValueAndValidity();
    }

    add(event: MatChipInputEvent): void {
        const input = event.input;
        const value = event.value;

        // Add our fruit
        if ((value || '').trim()) {
            this.emailList.push({ name: value.trim() });
        }

        this.formGroup.controls['emails'].setValue(this.emailList);
        this.chipList.errorState = this.formGroup.controls['emails'].status === 'INVALID' ? true : false;

        console.log(this.emailList);

        // Reset the input value
        if (input) {
            input.value = '';
        }
    }

    remove(fruit: any): void {
        const index = this.emailList.indexOf(fruit);

        if (index >= 0) {
            this.emailList.splice(index, 1);
        }
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
