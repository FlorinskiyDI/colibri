import { Component, ViewChild, OnInit } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
// import { Observable } from 'rxjs/Observable';

/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';
/* model-api */ import { SearchQueryApiModel, SearchQueryPage } from 'shared/models/entities/api/page-search-entry.api.model';
/* service */ import { UserService } from '../../../common/services/user.service';
/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'cmp-user-data-grid',
    templateUrl: './user-data-grid.component.html',
    styleUrls: ['./user-data-grid.component.scss'],
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class UserDataGridComponent implements OnInit {
    @ViewChild('dtUsers') dtUsers: any;
    userDialogOverviewConfig: DialogDataModel<any>;
    // table
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    tbSelectedColumns: any[];
    // option
    optionTbToggle: any = {
        columns: [
            { field: 'userName', header: 'User name', width: 330 },
            { field: 'email', header: 'Email', width: 300 },
            { field: 'emailConfirmed', header: 'Status in system', width: null }
        ],
        filter: false
    };

    constructor(
        private messageService: MessageService,
        private usersApiService: UsersApiService,
        private userService: UserService
    ) {
        this.tbSelectedColumns = this.optionTbToggle.columns;
        this.tbLoading = true;
    }

    ngOnInit() {
        // this.subscriberResetData = this.eventResetData.subscribe(() => this.dtGroups.reset());
    }
    ngOnDestroy() {
        // this.subscriberResetData.unsubscribe();
    }

    // item_viewDetails(id: string) {
    //     this.userDialogOverviewConfig = new DialogDataModel(true, { userId: id });
    // }
    item_delete(id: string) {
        // this.deleteItem.emit(groupId);
    }
    item_invite(id: string) {
        this.usersApiService.sendInvite(id).subscribe(
            (data: any) => {
                this.dtUsers.reset();
                this.messageService.add({ severity: 'success', summary: 'Success', detail: 'Group was removed successfully' });
            }
        );
        // this.deleteItem.emit(groupId);
    }

    tb_loadItems(event: any) {
        this.tbLoading = true;
        const searchEntry = {
            searchQueryPage: {
                pageNumber: event.first,
                pageLength: event.rows
            } as SearchQueryPage,
            orderStatement: (event.sortField && event.sortOrder)
                ? { columName: event.sortField, reverse: event.sortOrder > 0 }
                : null
        } as SearchQueryApiModel;
        this._requestGetRootGroups(searchEntry);
    }

    public checkIsExpired(emailConfirmInvitationDate: any, emailConfirmTokenLifespan: any) {
        if (emailConfirmInvitationDate && emailConfirmTokenLifespan) {
            const result = this.userService.checkIsExpired(emailConfirmInvitationDate, emailConfirmTokenLifespan);
            return result;
        }
        return false;
    }

    _requestGetRootGroups(searchEntry: SearchQueryApiModel) {
        this.tbLoading = true;
        this.usersApiService.getAll(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.itemList;
            this.tbTotalItemCount = response.totalItemCount;
        });
    }


    public userDialogOverviewOpen(id: any) { this.userDialogOverviewConfig = new DialogDataModel(true, { userId: id }); }
    public userDialogOverviewOnChange() { this.dtUsers.reset(); }
    public userDialogOverviewOnCancel() { console.log('userDialogOverviewOnCancel'); }
    public userDialogOverviewOnHide() { console.log('userDialogOverviewOnHide'); }
}
