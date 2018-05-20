import { Component } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-transfer */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupMembersApiService } from 'shared/services/api/group-members.api.service';

@Component({
    selector: 'group-member-grid-cmp',
    templateUrl: './group-member-grid.component.html',
    providers: [ConfirmationService, MessageService]
})
export class GroupMemberGridComponent {


    dialogGroupMemberAddConfig: DialogDataModel<any>;
    dialogGroupMemberDetailConfig: DialogDataModel<any>;
    selectedMember: any;

    gridItems: any[] = [];
    gridCols: any[] = [];
    gridFilter = false;
    drpdwnStatuses: any[] = [];

    constructor(
        private groupManageTransferService: GroupManageTransferService,
        private groupMembersApiService: GroupMembersApiService,
        private messageService: MessageService,
        private confirmationService: ConfirmationService
    ) {

        this.groupManageTransferService.getSelectedGroupId().subscribe((data: any) => {
            if (data) {
                this._requestGetMembers(data);
            }
        });

        this.gridItems = [
            { 'userName': 'user 1', 'email': 'user1@gmail.com', 'id': 1, 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 2', 'email': 'user2@gmail.com', 'id': 2, 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 3', 'email': 'user3@gmail.com', 'id': 3, 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 4', 'email': 'user4@gmail.com', 'id': 4, 'col1': 'col1', 'col2': 'col2' }
        ];

        this.gridCols = [
            { field: 'userName', header: 'User name' },
            { field: 'email', header: 'E-mail' },
            { field: 'col1', header: 'Col 1' },
            { field: 'col2', header: 'Col 2' }
        ];

        this.drpdwnStatuses = [
            {},
            { label: 'status 1', value: 'data' },
            { label: 'status 2', value: 'data' },
        ];
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

    public dialogGroupMemberAddOpen() { this.dialogGroupMemberAddConfig = new DialogDataModel<any>(true); }
    public dialogGroupMemberAddOnChange() {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Member was added successfully' });
    }
    public dialogGroupMemberAddOnCancel() { console.log('dialogGroupMemberAddOnCancel'); }
    public dialogGroupMemberAddOnHide() { console.log('dialogGroupMemberAddOnHide'); }

    public dialogGroupMemberDetailOpen(data: any) { this.dialogGroupMemberDetailConfig = new DialogDataModel<any>(true, data); }
    public dialogGroupMemberDetailOnHide() { console.log('dialogGroupMemberDetailOnHide'); }

    _requestGetMembers(data: any) {
        this.groupMembersApiService.getByGroup(data).subscribe(
            (response: Array<any>) => {
                console.log(response);
            });
    }
}
