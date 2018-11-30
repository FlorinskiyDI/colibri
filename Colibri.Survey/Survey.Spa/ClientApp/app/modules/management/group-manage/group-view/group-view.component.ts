import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cmp-group-view',
    templateUrl: './group-view.component.html',
    styleUrls: ['./group-view.component.scss'],
})

export class GroupViewComponent {
    items: MenuItem[];
    itemId: any;

    constructor(
        private route: ActivatedRoute,
    ) {
        this.route.params.subscribe((params: any) => {
            this.itemId = params['id'] ? params['id'] : null;
            console.log(this.itemId);
        });
        this.items = [
            { label: 'Information', routerLink: ['/manage/groups/' + this.itemId + '/detail'] },
            { label: 'Members of group', routerLink: ['/manage/groups/' + this.itemId + '/members'] },
        ];
    }
}
