import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs/Subject';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'cmp-access-general',
    templateUrl: 'access-manage.component.html',
    styleUrls: ['./access-manage.component.scss']
})

export class AccessManageComponent implements OnInit {

    eventResetData: Subject<any> = new Subject<any>();
    dialogCreateConfig: DialogDataModel<any>;
    itemId: any;
    view_OptionList: Array<any> = [{ label: 'MEMBERS', value: 'members' }, { label: 'ROLES', value: 'roles' }];

    constructor(
        private route: ActivatedRoute
    ) {
        this.route.parent.params.subscribe((params: any) => {
            this.itemId = params['id'] ? params['id'] : null;
        });
    }
    ngOnInit() {}

    public dialogCreateOpen() { this.dialogCreateConfig = new DialogDataModel<any>(true, { groupId: this.itemId }); }
    public dialogCreateOnChange() {
        debugger
        this.eventResetData.next();
    }
    public dialogCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogCreateOnHide() { console.log('dialogGroupCreateOnHide'); }
}
