import { Component } from '@angular/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'group-member-grid-cmp',
    templateUrl: './group-member-grid.component.html'
})
export class GroupMemberGridComponent {


    dialogGroupMemberAddConfig: DialogDataModel<any>;

    gridItems: any[] = [];
    gridCols: any[] = [];
    gridFilter = false;
    drpdwnStatuses: any[] = [];

    constructor() {
        this.gridItems = [
            { 'userName': 'user 1', 'email': 'user1@gmail.com', 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 2', 'email': 'user2@gmail.com', 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 3', 'email': 'user3@gmail.com', 'col1': 'col1', 'col2': 'col2' },
            { 'userName': 'user 4', 'email': 'user4@gmail.com', 'col1': 'col1', 'col2': 'col2' }
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

    public dialogGroupMemberAddOpen() { this.dialogGroupMemberAddConfig = new DialogDataModel<any>(true); }
    public dialogGroupMemberAddOnChange() { console.log('dialogGroupMemberAddOnChange'); }
    public dialogGroupMemberAddCancel() { console.log('dialogGroupMemberAddCancel'); }
    public dialogGroupMemberAddOnHide() { console.log('dialogGroupMemberAddOnHide'); }
}
