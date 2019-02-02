import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'cmp-layout-portal',
    templateUrl: 'layout-portal.component.html',
    styleUrls: ['./layout-portal.component.scss']
})

export class LayoutPortalComponent implements OnInit {
    eventSidebarToggle: Subject<any> = new Subject<any>();

    constructor() {}
    ngOnInit() {}

    sidebarToggle() {
        this.eventSidebarToggle.next();
    }
}
