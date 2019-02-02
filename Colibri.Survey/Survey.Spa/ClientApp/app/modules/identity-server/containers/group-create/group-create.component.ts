import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'cmp-group-create',
    templateUrl: './group-create.component.html',
    styleUrls: ['./group-create.component.scss']
})

export class GroupCreateComponent implements OnInit {

    constructor(public router: Router) { }
    ngOnInit() { }

}
