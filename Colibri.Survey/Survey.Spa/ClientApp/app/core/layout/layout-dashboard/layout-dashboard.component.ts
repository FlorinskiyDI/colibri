import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';


@Component({
    selector: 'cmp-layout-dashboard',
    templateUrl: 'layout-dashboard.component.html',
    styleUrls: ['./layout-dashboard.component.scss']
})

export class LayoutDashboardComponent implements OnInit {
    eventSidebarToggle: Subject<any> = new Subject<any>();

    constructor() {}
    ngOnInit() {}

    sidebarToggle() {
        this.eventSidebarToggle.next();
    }
}
