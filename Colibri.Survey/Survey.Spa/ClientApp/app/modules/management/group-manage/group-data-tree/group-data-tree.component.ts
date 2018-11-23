import { Component } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Router } from '@angular/router';

/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* model-api */ import { GroupApiModel } from 'shared/models/entities/api/group.api.model';
/* model-api */ import { PageSearchEntryApiModel, SearchEntryApiModel, PageFilterStatement } from 'shared/models/entities/api/page-search-entry.api.model';

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


export class GroupDataTreeComponent {

    // table
    tbColumns: any[];
    tbSelectedColumns: any[];
    selectedGroup: any;
    tbItems: any[] = [];
    tbCols: any[] = [];
    tbLoading = true;
    tbTotalItemCount: number;
    isNodeSelected = false;
    constructor(
        private router: Router,
        private groupsApiService: GroupsApiService,
    ) {
        this.tbColumns = [
            { field: 'name', header: 'Group name' },
            { field: 'countChildren', header: 'Number of subgroups' },
        ];
        this.tbSelectedColumns = this.tbColumns;
        this.tbLoading = true;
    }

    loadNodes(event: any) {
        this.tbLoading = true;
        const searchEntry = {
            pageNumber: event.first,
            pageLength: event.rows,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null

        } as PageSearchEntryApiModel;
        this._requestGetRootGroups(searchEntry);
    }

    onNodeExpand(event: any) {
        const node = event.node;
        const searchEntry = {
            pageNumber: event.first,
            pageLength: event.rows,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null

        } as PageSearchEntryApiModel;
        //
        this._requestGetSubGroups(node, searchEntry);
    }

    goToGroupView(groupId: string) {
        this.router.navigate(['/manage/groups/' + groupId]);
    }

    _requestGetRootGroups(searchEntry: PageSearchEntryApiModel) {
        this.tbLoading = true;
        this.groupsApiService.getRoot(searchEntry).subscribe((response: any) => {
            this.tbLoading = false;
            this.tbItems = response.items.map((item: GroupApiModel) => {
                return {
                    'label': item.name,
                    'data': item,
                    'leaf': false
                };
            });
            this.tbTotalItemCount = response.totalItemCount;
            this.selectedGroup = this.tbItems[0];
            // if (data.length > 0) {
            //     // this.groupManageTransferService.sendSelectedGroupId(data[0].id);
            // }
        });
    }

    _requestGetSubGroups(node: any, searchEntry: SearchEntryApiModel) {
        this.tbLoading = true;
        const that = this;
        this.groupsApiService.getSubgroups(searchEntry, node.data.id).subscribe((data: any) => {
            node.children = data.map((item: GroupApiModel) => { return { 'data': item, 'leaf': false }; });
            that.tbLoading = false;
            this.tbItems = [...this.tbItems];
        });
    }
}


export class FilterItem {
    public label: string;
    public filterStatement?: PageFilterStatement = new PageFilterStatement();
}

