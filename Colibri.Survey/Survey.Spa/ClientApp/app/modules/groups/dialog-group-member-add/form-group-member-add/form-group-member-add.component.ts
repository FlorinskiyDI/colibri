import { Component, Output, Input, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';


import { CHECK_EMAIL } from 'shared/helpers/check-email.helper';

@Component({
    selector: 'form-group-member-add-cmp',
    templateUrl: './form-group-member-add.component.html'
})

export class FormGroupMemberAddComponent {
    configData: any;
    @Output() onChange = new EventEmitter<any>();
    @Input()
    get config() { return this.configData; }
    set config(data: any) {
        if (data) {
            this.configData = data;
            this.formIsValid = true;
            this._cmpInitialize(data);

        }
    }

    formGroup: FormGroup;
    formIsValid = true;
    formIsValidEmail = true;

    constructor() {
        this.formBuild();
    }

    ngOnInit() { }
    ngOnDestroy() {
        this.onChange.unsubscribe();
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


    private formBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'members': new FormControl(data.name, [Validators.required])
        });
    }
    public formSubmit() {
        console.log('formSubmit');
        this.onChange.emit();
        this._cmpClear();
    }
    public formReset() {
        this._cmpClear();
    }

    private _cmpInitialize(data: FormGroupMemberAddConfig) {
        if (data) {
            console.log(data.groupId);
        }
    }
    private _cmpClear() {
        this.formIsValidEmail = true;
        this.formIsValid = true;
        this.formBuild();
    }
}

export class FormGroupMemberAddConfig {
    private _groupId: any[];
    get groupId(): any { return this._groupId; }

    constructor(
        groupId: any[]
    ) {
        this._groupId = groupId;
    }
}
