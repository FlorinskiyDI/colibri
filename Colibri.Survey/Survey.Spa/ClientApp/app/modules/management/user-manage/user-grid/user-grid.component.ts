import { Component, ViewChild , OnInit } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
// import { Observable } from 'rxjs/Observable';

/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';
/* model-api */ import { SearchQueryApiModel, SearchQueryPage } from 'shared/models/entities/api/page-search-entry.api.model';

@Component({
    selector: 'cmp-user-grid',
    templateUrl: './user-grid.component.html',
    styleUrls: ['./user-grid.component.scss'],
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class UserGridComponent implements OnInit {
    @ViewChild('dtUsers') dtUsers: any;
    // // // output events
    // // @Output() deleteItem = new EventEmitter<any>();
    // // @Output() editItem = new EventEmitter<any>();
    // // // input events
    // // @Input() eventResetData: Observable<any>;
    // // private subscriberResetData: any;

    // table
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    tbSelectedColumns: any[];
    // option
    optionTbToggle: any = {
        columns: [
            { field: 'userName', header: 'User name', width: 300 },
            { field: 'email', header: 'Email', width: 300 },
            { field: 'emailConfirmed', header: 'Status in system', width: null }
        ],
        filter: false
    };

    constructor(
        private messageService: MessageService,
        private usersApiService: UsersApiService,
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

    item_edit(id: string) {
        // this.editItem.emit(groupId);
    }
    item_delete(id: string) {
        // this.deleteItem.emit(groupId);
    }
    item_invite(id: string) {
        this.usersApiService.sendInvite(id).subscribe(
            (data: any) => {
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

    _requestGetRootGroups(searchEntry: SearchQueryApiModel) {
        this.tbLoading = true;
        this.usersApiService.getAll(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.itemList;
            this.tbTotalItemCount = response.totalItemCount;
        });
    }
}
