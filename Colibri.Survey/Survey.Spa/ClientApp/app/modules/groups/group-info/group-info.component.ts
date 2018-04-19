import { Component } from '@angular/core';
import { MessageService } from 'primeng/components/common/messageservice';

/* service */ import { GroupManageTransferService } from '../group-manage/group-manage.transfer.service';
/* component-config */ import { FormGroupUpdateConfig } from './form-group-update/form-group-update.component';

@Component({
    selector: 'group-info-cmp',
    templateUrl: './group-info.component.html',
    providers: [MessageService]
})
export class GroupInfoComponent {

    blockedPanel = true;
    formGroupUpdateConfig: FormGroupUpdateConfig;

    constructor(
        private messageService: MessageService,
        private groupManageTransferService: GroupManageTransferService
    ) {
        this.groupManageTransferService.getSelectedGroupId().subscribe((data: any) => {
            if (data) {
                this._cmpInitialize(data);
                this.blockedPanel = true;
            }
        });
    }

    private _cmpInitialize(data: any = null) {
        if (data) {
            const that = this;
            this._stub1().then(function (value: any) {
                that.formGroupUpdateConfig = new FormGroupUpdateConfig(value);
                that.blockedPanel = false;
            });
        }
    }
    private _cmpClear() { }

    public formGroupUpdateOnChange(data: any) {
        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was updated successfully' });
    }

    _stub1() {
        this.blockedPanel = true;
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { name: 'Group 1', id: '111' },
                    { name: 'Group 2', id: '222' },
                ];
                resolve(data);
            }, 1500);
        });
    }
}
