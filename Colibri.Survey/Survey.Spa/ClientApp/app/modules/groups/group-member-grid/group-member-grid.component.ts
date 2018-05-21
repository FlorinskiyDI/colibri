import { Component, ViewChild } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { LazyLoadEvent } from 'primeng/primeng';



/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupMembersApiService } from 'shared/services/api/group-members.api.service';

@Component({
    selector: 'group-member-grid-cmp',
    templateUrl: './group-member-grid.component.html',
    providers: [ConfirmationService, MessageService]
})
export class GroupMemberGridComponent {
    @ViewChild('dtGroupMembers') dtGroupMembers: any;

    dialogGroupMemberAddConfig: DialogDataModel<any>;
    dialogGroupMemberDetailConfig: DialogDataModel<any>;
    selectedMember: any;

    itemGroupId: string;
    gridItems: any[] = [];
    gridCols: any[] = [];
    gridFilter = false;
    gridLoading: boolean;
    drpdwnStatuses: any[] = [];

    constructor(
        private groupManageTransferService: GroupManageTransferService,
        private groupMembersApiService: GroupMembersApiService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {

        this.groupManageTransferService.getSelectedGroupId().subscribe((data: any) => {
            if (data) {
                this.itemGroupId = data;
                this._requestGetMembers(data);
                this.dtGroupMembers.reset();
            }
        });

        this.gridCols = [
            { field: 'id', header: 'id' },
            { field: 'email', header: 'email' }
        ];

        this.drpdwnStatuses = [
            {},
            { label: 'status 1', value: 'data' },
            { label: 'status 2', value: 'data' },
        ];
        this.gridLoading = true;
    }

    public loadGridLazy(event: LazyLoadEvent) {
        if (!this.itemGroupId) {
            return;
        }

        this.gridLoading = true;
        this.groupMembersApiService.getByGroup(this.itemGroupId).subscribe(
            (response: Array<any>) => {
                if (response) {
                    this.gridItems = response.slice(event.first, (event.first + event.rows));
                    this.gridLoading = false;
                }
            });
    }


    public memberUnsubscribe(data: any) {
        this.confirmationService.confirm({
            message: 'Are you sure that you want to unsubscribe this member?',
            accept: () => {
                this.selectedMember = null;
                this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Member was unsubscribed successfully' });
            }
        });
    }

    public dialogGroupMemberAddOpen() {
        this.dialogGroupMemberAddConfig = new DialogDataModel<any>(true, this.itemGroupId);
    }
    public dialogGroupMemberAddOnChange() {
        this.dtGroupMembers.reset();
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Member was added successfully' });
    }
    public dialogGroupMemberAddOnCancel() { console.log('dialogGroupMemberAddOnCancel'); }
    public dialogGroupMemberAddOnHide() { console.log('dialogGroupMemberAddOnHide'); }

    public dialogGroupMemberDetailOpen(data: any) { this.dialogGroupMemberDetailConfig = new DialogDataModel<any>(true, data); }
    public dialogGroupMemberDetailOnHide() { console.log('dialogGroupMemberDetailOnHide'); }

    _requestGetMembers(data: any) {

    }
}
