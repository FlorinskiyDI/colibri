import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
    selector: 'layout-portal-component',
    templateUrl: 'layout-portal.component.html',
    styleUrls: ['./layout-portal.component.scss']
})

export class LayoutPortalComponent implements OnInit {
    breadcrumbs$: any;
    simpleDrop = 0;
    constructor(
        // private router: Router,
        // private activatedRoute: ActivatedRoute
    ) {


    }
    ngOnInit() {

    }
}

