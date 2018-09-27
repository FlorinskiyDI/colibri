import { Component, ViewEncapsulation } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

// /* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* constant */ import { ModalTypes } from 'shared/constants/modal-types.constant';
/* directive */ import { ModalService } from 'shared/directives/modal/modal.service';

@Component({
    selector: 'cmp-group-grid',
    templateUrl: './group-grid.component.html',
    styleUrls: ['./group-grid.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class GroupGridComponent {
    // modal
    MODAL_GROUP_CREATE = ModalTypes.GROUP_CREATE;



    dialogGroupCreateConfig: DialogDataModel<any>;
    selectedGroup: any;
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = false;
    isNodeSelected = false;
    constructor(
        private modalService: ModalService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        // private groupManageTransferService: GroupManageTransferService,
        private groupsApiService: GroupsApiService,
    ) {
        this._requestGetRootGroups();
    }

    ngOninit() {
        this.tbCols = [
            { field: 'id', header: 'id' }
        ];
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
        this.groupsApiService.getSubGroups(event.node.data.id).subscribe((data: Array<GroupApiModel>) => {
            node.children = data.map((item: GroupApiModel) => { return { 'data': { 'id': item.id, 'name': item.name }, 'leaf': false }; });
            that.tbLoading = false;
            this.tbItems = [...this.tbItems];
        });
    }

    _requestGetRootGroups() {
        this.tbLoading = true;
        this.groupsApiService.getRoot().subscribe((data: Array<GroupApiModel>) => {
            this.tbItems = data.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id, 'name': item.name },
                    'leaf': false
                };
            });
            this.tbLoading = false;
            this.selectedGroup = this.tbItems[0];
            if (data.length > 0) {
                // this.groupManageTransferService.sendSelectedGroupId(data[0].id);
            }
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
