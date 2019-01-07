import { Component, Input, Output, EventEmitter, ViewChild , OnInit } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Observable } from 'rxjs/Observable';

import {
    Router,
    NavigationExtras
  } from '@angular/router';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-api */ import { SearchQueryApiModel, SearchQueryPage } from 'shared/models/entities/api/page-search-entry.api.model';

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
export class GroupDataGridComponent implements OnInit {
    @ViewChild('dtGroups') dtGroups: any;
    // output events
    @Output() deleteItem = new EventEmitter<any>();
    @Output() editItem = new EventEmitter<any>();
    // input events
    @Input() eventResetData: Observable<any>;
    private subscriberResetData: any;

    // table
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    tbSelectedColumns: any[];
    // option
    optionTbToggle: any = {
        columns: [
            // { field: 'name', header: 'Group name', width: 360, sort: true },
            { field: 'groupID', header: 'Identifier', width: 160, sort: true },
            { field: 'description', header: 'Description', width: null, sort: true }
        ],
        filter: false
    };

    constructor(
        private router: Router,
        private groupsApiService: GroupsApiService,
    ) {
        this.tbSelectedColumns = this.optionTbToggle.columns;
        this.tbLoading = true;
    }

    goToOverview(data1: any, data2: any) {
        debugger
        const navigationExtras: NavigationExtras = {
            queryParams: { 'session_id': data1 },
            fragment: data2,
            skipLocationChange: true
        };
        this.router.navigate(['is/groups/overview/' + data1], navigationExtras);
        window.history.pushState('', '', 'is/groups/overview/' + data1);
    }

    ngOnInit() {
        this.subscriberResetData = this.eventResetData.subscribe(() => this.dtGroups.reset());
    }
    ngOnDestroy() {
        this.subscriberResetData.unsubscribe();
    }

    item_edit(groupId: string) { this.editItem.emit(groupId); }
    item_delete(groupId: string) { this.deleteItem.emit(groupId); }

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
        this.groupsApiService.getAll(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.itemList;
            this.tbTotalItemCount = response.searchResultPage.totalItemCount;
        });
    }
}
