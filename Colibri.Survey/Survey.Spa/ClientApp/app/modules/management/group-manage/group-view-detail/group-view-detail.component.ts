import { Component, ViewChild} from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* helper */ import { FormGroupHelper } from 'shared/helpers/form-group.helper';

@Component({
    selector: 'cmp-group-view-detail',
    templateUrl: './group-view-detail.component.html',
    providers: [MessageService]
})

export class GroupViewDetailComponent {
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
        this.componentInit();
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

    // Form
    private formInitBuild(data: any = {}): void {
        this.formGroup = new FormGroup({
            'name': new FormControl(data.name, [Validators.required]),
            'groupID': new FormControl(null, [Validators.required]),
            'isSubgroup': new FormControl(null, [Validators.required]),
            'parentId': new FormControl(data.parentId),
            'description': new FormControl(data.description),
        });
    }
    // private formInitControlData() {
    //     this.groupsApiService.getAll(['id', 'name']).subscribe(
    //         (response: Array<GroupApiModel>) => {
    //             const groups = response && response.map((item: any) => { return { label: item.name, value: item.id }; });
    //             this.drpdwnGroups = this.drpdwnGroups.concat([{ label: 'none' }]).concat(groups);

    //         });
    // }
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
    public formCancel() { }
    // end form

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
        // const that = this;
        // this.groupsApiService.getSubgroups(new SearchQueryApiModel(), event.node.data.id).subscribe((data: Array<GroupApiModel>) => {
        //     event.node.children = data.map((item: GroupApiModel) => {
        //         return {
        //             'label': item.name,
        //             'data': { 'id': item.id },
        //             'leaf': false
        //         };
        //     });
        //     that.treeloading = false;
        // });
    }
}
