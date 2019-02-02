import { Component, ViewChild, Output, Input, EventEmitter, OnInit } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* model-api */ import { SearchQueryApiModel, SearchQueryPage } from 'shared/models/entities/api/page-search-entry.api.model';

@Component({
    selector: 'cmp-group-data-tree',
    templateUrl: './group-data-tree.component.html',
    styleUrls: ['./group-data-tree.component.scss'],
    providers: [
        TreeDragDropService,
        ConfirmationService,
        MessageService
    ]
})


export class GroupDataTreeComponent implements OnInit {
    @ViewChild('treeGroups') treeGroups: any;
    // output events
    @Output() deleteItem = new EventEmitter<any>();
    @Output() editItem = new EventEmitter<any>();
    // input events
    @Input() eventResetData: Observable<any>;
    private subscriberResetData: any;

    // table
    tbSelectedColumns: any[];
    selectedGroup: any;
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    isNodeSelected = false;

    // option
    optionTbToggle: any = {
        columns: [
            { field: 'name', header: 'Group name', width: 400, sort: true },
            { field: 'groupID', header: 'Identifier', width: 150, sort: true },
            { field: 'countChildren', header: 'Suborgs', width: 100, sort: false },
            { field: 'description', header: 'Description', width: null, sort: true },
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

    ngOnInit() {
        this.subscriberResetData = this.eventResetData.subscribe(() => this.treeGroups.reset());
    }
    ngOnDestroy() {
        this.subscriberResetData.unsubscribe();
    }

    loadNodes(event: any) {
        this.tbLoading = true;

        const searchEntry = {
            searchQueryPage: {
                pageNumber: event.first,
                pageLength: event.rows
            } as SearchQueryPage,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null
        } as SearchQueryApiModel;

        this._requestGetRootGroups(searchEntry);
    }

    item_edit(groupId: string) { this.editItem.emit(groupId); }
    item_delete(groupId: string) { this.deleteItem.emit(groupId); }

    onNodeExpand(event: any) {
        const node = event.node;
        const searchEntry = {
            searchQueryPage: {
                pageNumber: event.first,
                pageLength: event.rows
            } as SearchQueryPage,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null
        } as SearchQueryApiModel;
        //
        this._requestGetSubGroups(node, searchEntry);
    }

    goToGroupView(groupId: string) {
        this.router.navigate(['/manage/groups/' + groupId]);
    }

    _requestGetRootGroups(searchEntry: SearchQueryApiModel) {
        this.tbLoading = true;
        this.groupsApiService.getRoot(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.itemList.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': item,
                    'leaf': false
                };
            });
            this.tbTotalItemCount = response.searchResultPage.totalItemCount;
            this.selectedGroup = this.tbItems[0];
            // if (data.length > 0) {
            //     // this.groupManageTransferService.sendSelectedGroupId(data[0].id);
            // }
        });
    }

    _requestGetSubGroups(node: any, searchEntry: SearchQueryApiModel) {
        this.tbLoading = true;
        const that = this;
        this.groupsApiService.getSubgroups(searchEntry, node.data.id).subscribe((data: any) => {
            node.children = data.map((item: GroupApiModel) => { return { 'data': item, 'leaf': false }; });
            that.tbLoading = false;
            this.tbItems = [...this.tbItems];
        });
    }
}
