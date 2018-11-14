import { Component, ViewChild, ElementRef } from '@angular/core';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { FormControl } from '@angular/forms';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
// import { Observable } from 'rxjs/Observable';
// import { map, startWith } from 'rxjs/operators';
import { MatAutocompleteSelectedEvent, MatChipInputEvent, MatAutocomplete } from '@angular/material';

// /* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* model-api */ import { PageSearchEntryApiModel, PageFilterStatement } from 'shared/models/entities/api/page-search-entry.api.model';
/* constant */ import { ModalTypes } from 'shared/constants/modal-types.constant';
// /* directive */ import { ModalService } from 'shared/directives/modal/modal.service';

@Component({
    selector: 'cmp-group-data-tree',
    templateUrl: './group-data-tree.component.html',
    styleUrls: ['./group-data-tree.component.scss'],
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class GroupDataTreeComponent {
    // ///////////
    // ///////////
    // visible = true;
    chipSelectable = true;
    chipRemovable = true;
    chipAddOnBlur = true;
    chipSeparatorKeysCodes: number[] = [COMMA];
    chipFruitCtrl = new FormControl();
    filteredItems: any[];
    chipItems: FilterItem[] = [];
    // fruits: string[] = [];
    pageColumnNames: string[] = ['Name', 'Description'];

    @ViewChild('chipInput') chipInput: ElementRef<HTMLInputElement>;
    @ViewChild('auto') matAutocomplete: MatAutocomplete;
    // ///////////
    // ///////////



    // modal
    MODAL_GROUP_CREATE = ModalTypes.GROUP_CREATE;



    dialogGroupCreateConfig: DialogDataModel<any>;
    selectedGroup: any;
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    isNodeSelected = false;
    constructor(
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private groupsApiService: GroupsApiService,
    ) {
        // this._requestGetRootGroups();

        // this.filteredItems = this.chipFruitCtrl.valueChanges.pipe(
        //     startWith(null),
        //     map((fruit: string | null) => {
        //         return fruit ? this._filter(fruit) : this.allFruits.slice();
        //     }));

        this.chipFruitCtrl.valueChanges.subscribe((data: any) => {
            if (data === '') {
                this.filterItem = new FilterItem();
            }
            if (this.filterItem.filterStatement.propertyName) {
                data = data.split(':').pop();
            }
            this.filteredItems = data ? this._filter(data) : this.pageColumnNames.slice();
            // if (this.chipItems.length > 0) {
            //     this.filteredItems.unshift('OR');
            // }
        });
    }

    chipOnEnter(data: any) {
    }

    add(event: MatChipInputEvent): void {
        // debugger
        // console.log(event);
        // // debugger
        // // Add item only when MatAutocomplete is not open
        // // To make sure this does not conflict with OptionSelected Event
        // if (!this.matAutocomplete.isOpen) {
        //     console.log(this.matAutocomplete.isOpen);
        //     const input = event.input;
        //     let value = event.value;

        //     if ((value || '').trim()) {
        //         if (this.filterItem.filterStatement && this.filterItem.filterStatement.propertyName == null) {
        //             this.filterItem.filterStatement.propertyName = value;
        //             const lastChar = value.substr(value.length - 1);
        //             if (lastChar && lastChar !== ':') {
        //                 this.chipInput.nativeElement.value = value + ':';
        //             }

        //         } else {
        //             value = value.split(':').pop();
        //             if (value && value !== '') {
        //                 this.chipInput.nativeElement.value = null;
        //                 this.filterItem.filterStatement.value = value.split(':').pop();
        //                 this.chipItems.push({
        //                     label: this.filterItem.filterStatement.propertyName + ':' + this.filterItem.filterStatement.value,
        //                     filterStatement: Object.assign({}, this.filterItem.filterStatement)
        //                 });
        //                 this.filterItem = new FilterItem();
        //             }


        //         }
        //     }
        //     this.chipFruitCtrl.setValue(null);
        //     // Add our fruit
        //     // if ((value || '').trim()) {
        //     //     this.chipItems.push({ label: value.trim() });
        //     // }

        //     // // Reset the input value
        //     // if (input) {
        //     //     input.value = '';
        //     // }
        // }
    }

    remove(item: any): void {

        const index = this.chipItems.indexOf(item);
        this.filterItem = new FilterItem();
        this.chipFruitCtrl.setValue(null);
        if (index >= 0) {
            this.chipItems.splice(index, 1);
        }
    }

    filterItem = new FilterItem();
    selected(event: MatAutocompleteSelectedEvent): void {

        if (this.filterItem.filterStatement && this.filterItem.filterStatement.propertyName == null) {
            this.filterItem.filterStatement.propertyName = event.option.viewValue;
            this.chipInput.nativeElement.value = event.option.viewValue + ':';
        } else {
            this.chipInput.nativeElement.value = null;
            this.filterItem.filterStatement.value = event.option.viewValue;
            this.chipItems.push({
                label: this.filterItem.filterStatement.propertyName + ':' + this.filterItem.filterStatement.value,
                filterStatement: Object.assign({}, this.filterItem.filterStatement)
            });
            this.filterItem = new FilterItem();
            this.chipFruitCtrl.setValue(null);
        }

    }

    private _filter(value: string): string[] {
        const filterValue = value.toLowerCase();

        return this.pageColumnNames.filter(data => data.toLowerCase().indexOf(filterValue) === 0);
    }










    ngOninit() {
        this.tbCols = [
            { field: 'id', header: 'id' }
        ];

        this.tbLoading = true;
    }

    public createGroup() {
        this.dialogGroupCreateOpen();
    }
    public removeGroup(data: any) {
        console.log(`removeGroup - ${data.id}`);
        const that = this;
        this.confirmationService.confirm({
            message: 'Are you sure that you want to remove this group?',
            accept: () => {
                that.selectedGroup = null;
                this.groupsApiService.delete(data.id).subscribe(
                    (response: any) => {
                        this._requestGetSubGroups(true);
                        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was removed successfully' });
                    },
                    (error: any) => {
                        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error' });
                    }
                );
            }
        });
    }
    public selectGroup(data: any) {
        if (data.node && data.node.data && data.node.data.id) {
            // this.groupManageTransferService.sendSelectedGroupId(data.node.data.id);
        }
    }
    public searchGroups(data: any) {
        // const that = this;
        if (data === '') {
            this._requestGetSubGroups(true);
        } else {
            // this._stub2().then(function (value: any) {
            //     that.items = value;
            //     that.treeloading = false;
            //     that.selectedGroup = value[0];
            // });
        }
    }



    // #region - GROUP DIALOG actions
    public dialogGroupCreateOpen() { this.dialogGroupCreateConfig = new DialogDataModel<any>(true); }
    public dialogGroupCreateOnChange() {
        this._requestGetSubGroups(true);
        console.log('dialogGroupCreateOnChange');
    }
    public dialogGroupCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogGroupCreateOnHide() { console.log('dialogGroupCreateOnHide'); }
    // #endregion



    loadNodes(event: any) {
        this.tbLoading = true;
        const searchEntry = {
            pageNumber: event.first > 0 ? event.first : 1,
            pageLength: event.rows,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null

        } as PageSearchEntryApiModel;
        this._requestGetRootGroups(searchEntry);
    }

    onNodeSelect(event: any) {
        console.log(event.node.data.name);
        this.isNodeSelected = true;
    }

    onNodeUnselect(event: any) {
        this.isNodeSelected = false;
        console.log(event.node.data.name);
    }

    onNodeExpand(event: any) {
        const node = event.node;
        const that = this;
        //
        this.tbLoading = true;
        this.groupsApiService.getSubGroups(event.node.data.id).subscribe((data: any) => {
            node.children = data.map((item: GroupApiModel) => { return { 'data': { 'id': item.id, 'name': item.name }, 'leaf': false }; });
            that.tbLoading = false;
            this.tbItems = [...this.tbItems];
        });
    }

    goToGroupView(groupId: string) {
        this.router.navigate(['/manage/groups/' + groupId]);
    }

    _requestGetRootGroups(searchEntry: PageSearchEntryApiModel) {
        this.tbLoading = true;
        this.groupsApiService.getRoot(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.items.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id, 'name': item.name },
                    'leaf': false
                };
            });
            this.tbTotalItemCount = response.totalItemCount;
            this.selectedGroup = this.tbItems[0];
            // if (data.length > 0) {
            //     // this.groupManageTransferService.sendSelectedGroupId(data[0].id);
            // }
        });
    }

    _requestGetSubGroups(changeSelectedGroup: boolean, subGroupId: string = null) {
        this.tbLoading = true;
        this.groupsApiService.getSubGroups(subGroupId).subscribe((data: Array<GroupApiModel>) => {
            this.tbItems = data.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id },
                    'leaf': false
                };
            });
            this.tbLoading = false;

            if (changeSelectedGroup) {
                this.selectedGroup = this.tbItems[0];
                if (data.length > 0) {
                    // this.groupManageTransferService.sendSelectedGroupId(data[0].id);
                }
            }

        });
    }
}


export class FilterItem {
    public label: string;
    public filterStatement?: PageFilterStatement = new PageFilterStatement();
}

