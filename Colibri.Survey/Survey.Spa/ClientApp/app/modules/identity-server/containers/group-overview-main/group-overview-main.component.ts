import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'cmp-group-overview-main',
    templateUrl: 'group-overview-main.component.html',
    styleUrls: ['./group-overview-main.component.scss']
})

export class GroupOverviewMainComponent implements OnInit {
    itemId: any;    

    constructor(
        private route: ActivatedRoute
    ) {
        this.route.parent.params.subscribe((params: any) => {
            this.itemId = params['id'] ? params['id'] : null;
        });
    }
    ngOnInit() {}
}
