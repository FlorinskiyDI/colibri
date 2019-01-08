import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cmp-group-overview',
    templateUrl: 'group-overview.component.html',
    styleUrls: ['./group-overview.component.scss']
})

export class GroupOverviewComponent implements OnInit {
    menuItems: any[] = [];
    itemId: any;

    constructor(private route: ActivatedRoute) {}

    ngOnInit() {
        this.route.params.subscribe((params: any) => {
            this.itemId = params['id'] ? params['id'] : null;
            console.log(this.itemId);
        });
        this.menuItems = [
            { label: 'OVERVIEW', routerLink: ['main'] },
            { label: 'MEMBERS', routerLink: ['members'] },
            { label: 'TEST ROUTE', routerLink: ['1'] },
            // { label: 'TITLE 2', routerLink: ['2'] },
        ];
    }
}
