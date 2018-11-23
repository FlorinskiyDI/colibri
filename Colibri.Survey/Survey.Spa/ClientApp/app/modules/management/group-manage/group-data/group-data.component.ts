import { Component } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Subject } from 'rxjs/Subject';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';

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
    eventsSubject: Subject<any> = new Subject<any>();
    // view option for displaing group data
    view_OptionList: Array<any> = [{ label: 'list', value: 'list' }, { label: 'tree', value: 'tree' }];
    view_Option = 'list';

    constructor(
        private messageService: MessageService,
        private confirmationService: ConfirmationService,
        private groupsApiService: GroupsApiService,
    ) {
    }

    ngOninit() {

    }


    // option_change() {
    //     this.eventsSubject = new Subject<any>();
    // }

    item_delete(groupId: string) {
        this.confirmationService.confirm({
            message: 'Are you sure that you want to remove this group?',
            accept: () => {
                this.groupsApiService.delete(groupId).subscribe(
                    (response: any) => {
                        this.eventsSubject.next();
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
        console.log(groupId);
    }
}
