import { Component, ViewEncapsulation } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

/* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';

@Component({
    selector: 'group-tree-cmp',
    templateUrl: './group-tree.component.html',
    styleUrls: ['./group-tree.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class GroupTreeComponent {

    dialogGroupCreateConfig: DialogDataModel<any>;
    selectedGroup: any;
    treeItems: any[] = [];
    treeloading = false;

    constructor(
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private groupManageTransferService: GroupManageTransferService,
        private groupsApiService: GroupsApiService,
    ) {
        this._requestGetGroups(true);
    }

    public createGroup() { this.dialogGroupCreateOpen(); }
    public removeGroup(data: any) {
        console.log(`removeGroup - ${data.id}`);
        const that = this;
        this.confirmationService.confirm({
            message: 'Are you sure that you want to remove this group?',
            accept: () => {
                that.selectedGroup = null;
                this.groupsApiService.delete(data.id).subscribe(
                    (response: any) => {
                        this._requestGetGroups(true);
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
            this.groupManageTransferService.sendSelectedGroupId(data.node.data.id);
        }
    }
    public searchGroups(data: any) {
        // const that = this;
        if (data === '') {
            this._requestGetGroups(true);
        } else {
            // this._stub2().then(function (value: any) {
            //     that.treeItems = value;
            //     that.treeloading = false;
            //     that.selectedGroup = value[0];
            // });
        }
    }

    public dialogGroupCreateOpen() { this.dialogGroupCreateConfig = new DialogDataModel<any>(true); }
    public dialogGroupCreateOnChange() {
        this._requestGetGroups(true);
        console.log('dialogGroupCreateOnChange');
    }
    public dialogGroupCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogGroupCreateOnHide() { console.log('dialogGroupCreateOnHide'); }

    loadNode(event: any) {
        this.treeloading = true;
        const that = this;
        this.groupsApiService.getSubGroups(event.node.data.id).subscribe((data: Array<GroupApiModel>) => {
            event.node.children = data.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id },
                    'leaf': item.parentId ? true : false
                };
            });
            that.treeloading = false;
        });
    }

    _requestGetGroups(changeSelectedGroup: boolean, subGroupId: string = null) {
        this.treeloading = true;
        this.groupsApiService.getSubGroups(subGroupId).subscribe((data: Array<GroupApiModel>) => {
            this.treeItems = data.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': { 'id': item.id },
                    'leaf': item.parentId ? true : false
                };
            });
            this.treeloading = false;

            if (changeSelectedGroup) {
                this.selectedGroup = this.treeItems[0];
                if (data.length > 0) {
                    this.groupManageTransferService.sendSelectedGroupId(data[0].id);
                }
            }

        });
    }
}
