import { Component, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BlockableUI } from 'primeng/primeng';

@Component({
    selector: 'form-group-create-cmp',
    templateUrl: './form-group-create.component.html'
})

export class FormGroupCreateComponent implements BlockableUI {
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
    drpdwnGroups: any[] = [];

    constructor(
        private el: ElementRef
    ) {
        this.formBuild();
    }

    ngOnInit() { }
    ngOnDestroy() {
        this.onChange.unsubscribe();
    }

    private formBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'name': new FormControl(data.name, [Validators.required]),
            'parentId': new FormControl(data.parentId),
            'description': new FormControl(data.description),
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

    private _cmpInitialize(data: FormGroupCreateConfig) {
        if (data) {
            const groups = data.groups.map((item: any) => { return { label: item.name, value: item.id }; });
            this.drpdwnGroups = [{ label: 'None' }];
            this.drpdwnGroups = this.drpdwnGroups.concat(groups);
            console.log(data);
        }
    }
    private _cmpClear() {
        this.formIsValid = true;
        this.formBuild();
    }

    getBlockableElement(): HTMLElement {
        return this.el.nativeElement.children[0];
    }
}

export class FormGroupCreateConfig {
    private _groups: any[];
    get groups(): any { return this._groups; }

    constructor(
        groups: any[]
    ) {
        this._groups = groups;
    }
}
