import { Component, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BlockableUI } from 'primeng/primeng';

/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';

@Component({
    selector: 'cmp-group-detail',
    templateUrl: './group-detail.component.html'
})

export class GroupDetailComponent implements BlockableUI {
    configData: FormGroupUpdateConfig;
    @ViewChild('formGroupUpdate') formGroupUpdate: any;

    formGroup: FormGroup;
    formIsValid = true;
    drpdwnGroups: any[] = [];

    constructor(
        private groupsApiService: GroupsApiService,
        private el: ElementRef
    ) {
        this.formBuild();
    }

    ngOnInit() { }
    ngOnDestroy() {
    }

    private formBuild(data: GroupApiModel = new GroupApiModel()): void {
        this.formGroup = new FormGroup({
            'name': new FormControl(data.name, [Validators.required]),
            'parentId': new FormControl(data.parentId),
            'description': new FormControl(data.description),
        });
    }

    public formSubmit() {
        if (!this.formGroupUpdate.valid) {
            this.formIsValid = false;
            console.log('(FormGroupUpdateComponent) Form is NOT VALID!!!');
            return;
        }

        const group = Object.assign(
            {},
            { id: this.configData.item.id },
            this.formGroupUpdate.value
        ) as GroupApiModel;
        this.groupsApiService.update(group)
            .subscribe((response: GroupApiModel) => {
                this.configData.setItem(group);
            });
    }
    public formCancel() {
        this.formBuild(this.configData.item);
    }

    public _cmpInitialize(data: FormGroupUpdateConfig) {
        if (data) {
            const groups = data.groups.map((item: any) => { return { label: item.name, value: item.id }; });
            this.drpdwnGroups = [{ label: 'None' }];
            this.drpdwnGroups = this.drpdwnGroups.concat(groups);
            this.formBuild(data.item);
            console.log(data);
        }
    }
    // private _cmpClear() {
    //     this.formIsValid = true;
    //     this.formBuild();
    // }

    getBlockableElement(): HTMLElement {
        return this.el.nativeElement.children[0];
    }
}

export class FormGroupUpdateConfig {
    private _groups: any[];
    private _item: GroupApiModel;
    get groups(): any { return this._groups; }
    get item(): GroupApiModel { return this._item; }

    constructor(
        groups: any[],
        item: GroupApiModel
    ) {
        this._groups = groups;
        this._item = item;
    }

    setItem(item: GroupApiModel) {
        this._item = item;
    }
}
