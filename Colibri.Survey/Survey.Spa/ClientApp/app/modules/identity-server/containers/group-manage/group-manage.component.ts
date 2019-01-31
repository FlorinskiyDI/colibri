import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Subject } from 'rxjs/Subject';
import { Router } from '@angular/router';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
// /* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';


@Component({
    selector: 'cmp-group-manage',
    templateUrl: 'group-manage.component.html',
    styleUrls: ['./group-manage.component.scss'],
    providers: [
        ConfirmationService,
        MessageService
    ]
})

export class GroupManageComponent implements OnInit {
    eventResetData: Subject<any> = new Subject<any>();
    view_OptionList: Array<any> = [{ label: 'list', value: 'list' }, { label: 'tree', value: 'tree' }];
    view_Option = 'list';

    constructor(
        private router: Router,
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private groupsApiService: GroupsApiService,
    ) {
    }

    ngOnInit() { }

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
}
