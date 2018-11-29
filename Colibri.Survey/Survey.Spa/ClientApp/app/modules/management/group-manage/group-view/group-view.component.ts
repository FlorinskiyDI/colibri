import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
    selector: 'cmp-group-view',
    templateUrl: './group-view.component.html',
    styleUrls: ['./group-view.component.scss'],
})

export class GroupViewComponent {
    items: MenuItem[];

    constructor() {
        this.items = [
            {label: 'Main details', routerLink: ['/manage/groups/205b91ea-4e5c-e811-9c5c-d017c2aa438d/detail']},
            {label: 'Group members'},
        ];
    }
}
