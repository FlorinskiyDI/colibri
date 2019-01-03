import { Component, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Component({
    selector: 'cmp-layout-identity',
    templateUrl: 'layout-identity.component.html',
    styleUrls: ['./layout-identity.component.scss']
})

export class LayoutIdentityComponent implements OnInit {
    eventSidebarToggle: Subject<any> = new Subject<any>();

    constructor() {}
    ngOnInit() {}

    sidebarToggle() {
        this.eventSidebarToggle.next();
    }
}
