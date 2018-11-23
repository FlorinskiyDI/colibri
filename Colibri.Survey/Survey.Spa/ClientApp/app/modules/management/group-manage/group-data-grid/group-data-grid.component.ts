import { Component } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-api */ import { PageSearchEntryApiModel } from 'shared/models/entities/api/page-search-entry.api.model';

@Component({
    selector: 'cmp-group-data-grid',
    templateUrl: './group-data-grid.component.html',
    styleUrls: ['./group-data-grid.component.scss'],
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})
export class GroupDataGridComponent {
    // table
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    tbColumns: any[];
    tbSelectedColumns: any[];
    // option
    selectF = false;

    constructor(
        private groupsApiService: GroupsApiService,
    ) {
        this.tbColumns = [{ field: 'name', header: 'Group name' }];
        this.tbSelectedColumns = this.tbColumns;
        this.tbLoading = true;
    }

    ngOninit() { }

    tbloadItems(event: any) {
        this.tbLoading = true;
        const searchEntry = {
            pageNumber: event.first,
            pageLength: event.rows,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null

        } as PageSearchEntryApiModel;
        this._requestGetRootGroups(searchEntry);
    }

    _requestGetRootGroups(searchEntry: PageSearchEntryApiModel) {
        this.tbLoading = true;
        this.groupsApiService.getAll(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.items;
            this.tbTotalItemCount = response.totalItemCount;
        });
    }
}
