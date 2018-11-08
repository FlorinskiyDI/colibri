import { Component, Output, Input, EventEmitter, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BlockableUI } from 'primeng/primeng';

/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';

@Component({
    selector: 'cmp-group-form-create',
    templateUrl: './group-form-create.component.html'
})

export class GroupFormCreateComponent implements BlockableUI {
    @ViewChild('formGroupCreate') formGroupCreate: any;
    configData: any;
    @Output() onChange = new EventEmitter<any>();
    @Input()
    get config() { return this.configData; }
    set config(data: any) {
        debugger
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
        private el: ElementRef,
        private groupsApiService: GroupsApiService
    ) {
        this.formBuild();
    }

    ngOnInit() { }
    ngOnDestroy() {
        debugger
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
        if (!this.formGroupCreate.valid) {
            this.formIsValid = false;
            console.log('(FormGroupCreateComponent) Form is NOT VALID!!!');
            return;
        }

        const group = Object.assign({}, this.formGroupCreate.value);
        this.groupsApiService.create(group).subscribe((response: Array<GroupApiModel>) => {
            this.onChange.emit();
            this._cmpClear();
        });
    }
    public formReset() {
        this._cmpClear();
    }

    private _cmpInitialize(data: GroupFormCreateConfig) {
        if (data) {
            const groups = data.groups.map((item: any) => { return { label: item.name, value: item.id }; });
            this.drpdwnGroups = [{ label: 'none' }];
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

export class GroupFormCreateConfig {
    private _groups: any[];
    get groups(): any { return this._groups; }

    constructor(
        groups: any[]
    ) {
        this._groups = groups;
    }
}
