import { Component } from '@angular/core';

@Component({
    selector: 'group-member-grid-cmp',
    templateUrl: './group-member-grid.component.html'
})
export class GroupMemberGridComponent {

    gridItems: any[] = [];
    gridCols: any[] = [];
    gridFilter: boolean = false;
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
            { },
            { label: 'status 1', value: 'data' },
            { label: 'status 2', value: 'data' },
        ]
    }
}
