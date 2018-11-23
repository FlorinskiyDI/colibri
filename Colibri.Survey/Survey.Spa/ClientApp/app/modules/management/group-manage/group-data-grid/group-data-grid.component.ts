import { Component, Input, Output, EventEmitter, ViewChild , OnInit } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/components/common/messageservice';
import { Observable } from 'rxjs/Observable';

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
export class GroupDataGridComponent implements OnInit {
    @ViewChild('dtGroups') dtGroups: any;
    // output events
    @Output() deleteItem = new EventEmitter<any>();
    @Output() editItem = new EventEmitter<any>();
    // input events
    @Input() events: Observable<any>;
    private eventsSubscription: any;

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
        // this.eventsSubscription =  this.events.subscribe(() => this.event_tbReset());
    }

    ngOnInit() {
        this.eventsSubscription = this.events.subscribe(() => this.event_tbReset());
    }
    ngOnDestroy() {
        this.eventsSubscription.unsubscribe();
    }

    item_edit(groupId: string) { this.editItem.emit(groupId); }
    item_delete(groupId: string) { this.deleteItem.emit(groupId); }

    tb_loadItems(event: any) {
        this.tbLoading = true;
        const searchEntry = {
            pageNumber: event.first,
            pageLength: event.rows,
            orderStatement: (event.sortField && event.sortOrder) ? { columName: event.sortField, reverse: event.sortOrder > 0 } : null

        } as PageSearchEntryApiModel;
        this._requestGetRootGroups(searchEntry);
    }
    event_tbReset() {
        this.dtGroups.reset();
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
