import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
@Component({
    selector: 'cmp-user-manage',
    templateUrl: 'user-manage.component.html',
    styleUrls: ['./user-manage.component.scss']
})

export class UserManageComponent implements OnInit {

    userDialogInviteConfig: DialogDataModel<any>;
    constructor(
        public router: Router
    ) {}
    ngOnInit() {}

    public userDialogInviteOpen() { this.userDialogInviteConfig = new DialogDataModel(true); }
    public userDialogInviteOnChange() { console.log('userDialogInviteOnChange'); }
    public userDialogInviteOnCancel() { console.log('userDialogInviteOnCancel'); }
    public userDialogInviteOnHide() { console.log('userDialogInviteOnHide'); }
}
