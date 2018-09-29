import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
    selector: 'cmp-group-view',
    templateUrl: './group-view.component.html',
})

export class GroupViewComponent {
    items: MenuItem[];

    constructor() {
        this.items = [
            {label: 'Stats', icon: 'fa fa-fw fa-bar-chart'},
            {label: 'Calendar', icon: 'fa fa-fw fa-calendar'},
            {label: 'Documentation', icon: 'fa fa-fw fa-book'},
            {label: 'Support', icon: 'fa fa-fw fa-support'},
            {label: 'Social', icon: 'fa fa-fw fa-twitter', routerLink: ['/manage/groups/205b91ea-4e5c-e811-9c5c-d017c2aa438d/detail']}
        ];
    }
}
