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
            { label: 'MANAGE ACCESS', routerLink: ['access'] },
            { label: 'TEST ROUTE 1', routerLink: ['1'] },
            { label: 'TEST ROUTE 2', routerLink: ['1'] },
            // { label: 'TITLE 2', routerLink: ['2'] },
        ];
    }
}
