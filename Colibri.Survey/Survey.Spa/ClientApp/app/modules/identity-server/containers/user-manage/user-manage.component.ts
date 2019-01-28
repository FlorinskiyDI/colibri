import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'cmp-user-manage',
    templateUrl: 'user-manage.component.html',
    styleUrls: ['./user-manage.component.scss']
})

export class UserManageComponent implements OnInit {

    constructor(
        public router: Router
    ) {}
    ngOnInit() {}
}
