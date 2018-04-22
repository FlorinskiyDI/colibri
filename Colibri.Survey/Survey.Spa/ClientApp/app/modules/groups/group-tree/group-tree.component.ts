import { Component, ViewEncapsulation } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';


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
        // const that = this;
        // this._stub1().then(function (data: any) {
        //     that.treeItems = data;
        //     that.treeloading = false;
        //     that.selectedGroup = data[0];
        //     that.groupManageTransferService.sendSelectedGroupId(data[0].data.id);
        // });

        this._requestGetGroups();
    }

    public createGroup() { this.dialogGroupCreateOpen(); }
    public removeGroup(data: any) {
        console.log(`removeGroup - ${data.id}`);
        const that = this;
        this.confirmationService.confirm({
            message: 'Are you sure that you want to remove this group?',
            accept: () => {
                that.selectedGroup = null;
                this._stub1().then(function (value: any) {
                    that.treeItems = value;
                    that.treeloading = false;
                    that.selectedGroup = value[0];
                });
                this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was removed successfully' });
            }
        });
    }
    public selectGroup(data: any) {
        if (data.node && data.node.data && data.node.data.id) {
            this.groupManageTransferService.sendSelectedGroupId(data.node.data.id);
        }
    }
    public searchGroups(data: any) {
        const that = this;
        if (data === '') {
            this._stub1().then(function (value: any) {
                that.treeItems = value;
                that.selectedGroup = null;
                that.treeloading = false;
                that.selectedGroup = value[0];
            });
        } else {
            this._stub2().then(function (value: any) {
                that.treeItems = value;
                that.selectedGroup = null;
                that.treeloading = false;
                that.selectedGroup = value[0];
            });
        }
    }

    public dialogGroupCreateOpen() { this.dialogGroupCreateConfig = new DialogDataModel<any>(true); }
    public dialogGroupCreateOnChange() { console.log('dialogGroupCreateOnChange'); }
    public dialogGroupCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogGroupCreateOnHide() { console.log('dialogGroupCreateOnHide'); }


    _requestGetGroups() {
        this.groupsApiService.getAll();
        // this.groupsApiService.getAll().subscribe((data: any) => {
        //     debugger
        //     console.log(data);
        //     this.treeItems = data;
        //     this.treeloading = false;
        //     this.selectedGroup = data[0];
        //     this.groupManageTransferService.sendSelectedGroupId(data[0].data.id);
        // });
    }

    loadNode(event: any) {
        this.treeloading = true;
        const that = this;
        this._stub1().then(function (value: any) {
            event.node.children = value;
            that.treeloading = false;
        });
    }

    _stub1() {
        this.treeloading = true;
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { 'label': 'Test log name for node 1', 'data': { 'id': 1 }, 'leaf': false },
                    { 'label': 'Test log name for node 2', 'data': { 'id': 2 }, 'leaf': false },
                    { 'label': 'Test log name for node 3', 'data': { 'id': 3 }, 'leaf': false }
                ];
                resolve(data);
            }, 1500);
        });
    }

    _stub2() {
        this.treeloading = true;
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { 'label': 'Test log name for node 1', 'data': { 'id': 1 } },
                    { 'label': 'Test log name for node 2', 'data': { 'id': 2 } },
                ];
                resolve(data);
            }, 1500);
        });
    }

}
