import { Component, Output, Input, EventEmitter, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, FormArray } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';
import * as XLSX from 'xlsx';

/* helper */ import { CHECK_EMAIL } from 'shared/helpers/check-email.helper';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-api */ import { GroupMembersApiService } from 'shared/services/api/group-members.api.service';
// /* helper */ import { FormGroupHelper } from 'shared/helpers/form-group.helper';
// import { combineLatest } from 'rxjs/operators';

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
    fileIsNotValid = false;

    isFillByFile = false;
    // form variable
    @ViewChild('ngFormGroup') ngFormGroup: any;
    formGroup: FormGroup;
    formIsValid = true;
    formIsValidEmail = true;
    configData: any;
    uploadedFiles: any[] = [];
    fileName: any;

    constructor(
        private messageService: MessageService,
        private fb: FormBuilder,
        private apiService: GroupMembersApiService
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

    uploadHandler2(data: any) {
        console.log('uploadHandler');
        console.log(data);
    }

    onSelect(event: any) {
        let arrayBuffer: any;
        const file = event.files[0];
        this.fileName = file.name;
        const fileReader = new FileReader();
        fileReader.onload = (e) => {
            arrayBuffer = fileReader.result;
            const data = new Uint8Array(arrayBuffer);
            const arr = new Array();
            for (let i = 0; i !== data.length; ++i) { arr[i] = String.fromCharCode(data[i]); }
            const bstr = arr.join('');
            const workbook = XLSX.read(bstr, { type: 'binary' });
            const first_sheet_name = workbook.SheetNames[0];
            const worksheet = workbook.Sheets[first_sheet_name];
            const list = XLSX.utils.sheet_to_json(worksheet, { raw: true });
            const newList: any[] = [];
            list.forEach((element: any) => {
                if (element.userEmail) {
                    newList.push(element.userEmail);
                }
            });

            if (newList.length === 0) {
                this.formGroup.get('importData').setValue(null);
                this.formGroup.get('importData').updateValueAndValidity();
                const emailArray = this.formGroup.controls['emailArray'] as FormArray;
                emailArray.controls = [];
            } else {
                this.formGroup.get('importData').setValue({});
                this.formGroup.get('importData').updateValueAndValidity();

                const emailArray = this.formGroup.controls['emailArray'] as FormArray;
                newList.forEach(element => {
                    const control = this.fb.group({ 'email': [element, Validators.required] }) as FormGroup;
                    control.markAsTouched();
                    emailArray.push(control);
                });

            }
            console.log(newList);
        };
        fileReader.readAsArrayBuffer(file);
    }

    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'importData': new FormControl({}, [Validators.required]),
            'emailArray': this.fb.array([]),
        });

        this.formGroup.get('importData').markAsTouched();
        // add first element by default
        const emailArray = this.formGroup.controls['emailArray'] as FormArray;
        emailArray.push(this.fb.group({ 'email': [null] }));
        this.formGroup.get('importData').updateValueAndValidity();

        this.formGroup.statusChanges.subscribe((val: any) => {
            console.log(val);
        });

    }
    public formSubmit() {
        if (!this.formGroup.valid) {
            console.log(this.ngFormGroup);
            // const emailArray = this.formGroup.controls['emailArray'] as FormArray;
            // for (let index = 0; index < emailArray.controls.length - 1; index++) {
            //     const ctrl = emailArray.controls[index] as FormControl;
            //     ctrl.markAsTouched();
            // }
            // FormGroupHelper.setFormControlsAsTouched(this.formGroup);
            return;
        } else {
            const emailList: any[] = [];
            this.ngFormGroup.value.emailArray.forEach((element: any) => {
                if (element.email) {
                    emailList.push(element.email);
                }
            });
            this.apiService.addMultiple(this.dialogConfig.extraData.groupId, emailList)
                .subscribe((val: any) => {
                    this.dialogChange();
                    this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was created successfully' });
                });
            console.log('VALID');
        }
    }

    addNewControl(item: FormGroup) {
        // if (item.touched) {
        //     item.get('email').markAsTouched();
        //     this.formGroup.get('emailArray').updateValueAndValidity();
        // }
        item.get('email').setValidators([Validators.required]);
        item.get('email').updateValueAndValidity();
        console.log(item.touched);
        if (!item.touched) {
            const emailArray = this.formGroup.controls['emailArray'] as FormArray;
            const control = this.fb.group({ 'email': [null] }) as FormGroup;
            emailArray.push(control);
        }
    }

    onChckboxChange(data: any) {
        this.isFillByFile = data;
        if (data) {
            const emailArray = this.formGroup.get('emailArray') as FormArray;
            emailArray.controls = [];
        } else {
            const emailArray = this.formGroup.controls['emailArray'] as FormArray;
            const control = this.fb.group({ 'email': [null] }) as FormGroup;
            emailArray.push(control);
        }
    }

    removeEmail(item: any) {
        const emailArray = this.formGroup.get('emailArray') as FormArray;
        const index = emailArray.controls.indexOf(item);
        if (index !== -1) {
            emailArray.controls.splice(index, 1);
        }
    }
    getIndex(item: any) {
        const emailArray = this.formGroup.get('emailArray') as FormArray;
        return emailArray.controls.indexOf(item);
    }

    public formReset() {
        this.componentClear();
    }

    getTemplates() {
        const link = document.createElement('a');
        link.download = 'template.xlsx';
        link.href = 'files/template.xlsx';
        link.click();
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
