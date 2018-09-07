import { Component } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';

import { forkJoin } from 'rxjs';

/* service */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* component-config */ import { FormGroupUpdateConfig } from './form-group-update/form-group-update.component';

@Component({
    selector: 'group-info-cmp',
    templateUrl: './group-info.component.html',
    providers: [MessageService]
})
export class GroupInfoComponent {

    blockedPanel = false;
    formGroupUpdateConfig: FormGroupUpdateConfig;

    constructor(
        private messageService: MessageService,
        private groupsApiService: GroupsApiService,
        private groupManageTransferService: GroupManageTransferService
    ) {
        this.groupManageTransferService.getSelectedGroupId().subscribe((data: any) => {
            if (data) {
                this._cmpInitialize(data);
                this.blockedPanel = true;
            }
        });
    }

    private _cmpInitialize(data: any) {
        if (data) {
            forkJoin(
                this.groupsApiService.getAll(['id', 'name']),
                this.groupsApiService.get(data)
            ).subscribe((response: any[]) => {
                this.formGroupUpdateConfig = new FormGroupUpdateConfig(response[0], response[1]);
                this.blockedPanel = false;
            });
        }
    }
    // private _cmpClear() { }

    public formGroupUpdateOnChange(data: any) {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was updated successfully' });
    }

    // _stub1() {
    //     this.blockedPanel = true;
    //     return new Promise(function (resolve, reject) {
    //         window.setTimeout(function () {
    //             const data = [
    //                 { name: 'Group 1', id: '111' },
    //                 { name: 'Group 2', id: '222' },
    //             ];
    //             resolve(data);
    //         }, 1500);
    //     });
    // }
}
