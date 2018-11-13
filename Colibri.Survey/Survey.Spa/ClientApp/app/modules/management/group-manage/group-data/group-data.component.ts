import { Component } from '@angular/core';

@Component({
    selector: 'cmp-group-data',
    templateUrl: './group-data.component.html',
    styleUrls: ['./group-data.component.scss'],
})
export class GroupDataComponent {

    // view option for displaing group data
    view_OptionList: Array<any> = [{ label: 'list', value: 'list' }, { label: 'tree', value: 'tree' }];
    view_Option = 'tree';

    constructor() { }
}
