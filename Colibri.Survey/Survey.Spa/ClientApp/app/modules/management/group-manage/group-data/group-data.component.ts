import { Component } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'cmp-group-data',
    templateUrl: './group-data.component.html',
    styleUrls: ['./group-data.component.scss'],
    providers: [
        ConfirmationService,
        MessageService
    ]
})
export class GroupDataComponent {
    eventResetData: Subject<any> = new Subject<any>();
    dialogGroupCreateConfig: DialogDataModel<any>;
    view_OptionList: Array<any> = [{ label: 'list', value: 'list' }, { label: 'tree', value: 'tree' }];
    view_Option = 'list';

    constructor(
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private groupsApiService: GroupsApiService,
    ) {
    }

    ngOninit() { }

    item_delete(groupId: string) {
        this.confirmationService.confirm({
            message: 'Are you sure that you want to remove this group?',
            accept: () => {
                this.groupsApiService.delete(groupId).subscribe(
                    (response: any) => {
                        this.eventResetData.next();
                        this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was removed successfully' });
                    },
                    (error: any) => {
                        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error' });
                    }
                );
            }
        });
    }
    item_edit(groupId: string) {
        this.router.navigate(['manage/groups/' + groupId + '/detail']);
    }

    public dialogGroupCreateOpen() { this.dialogGroupCreateConfig = new DialogDataModel<any>(true); }
    public dialogGroupCreateOnChange() { this.eventResetData.next(); }
    public dialogGroupCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogGroupCreateOnHide() { console.log('dialogGroupCreateOnHide'); }
}
