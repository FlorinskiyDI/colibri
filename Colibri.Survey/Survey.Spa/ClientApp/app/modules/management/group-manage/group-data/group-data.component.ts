import { Component } from '@angular/core';

@Component({
    selector: 'cmp-group-data',
    templateUrl: './group-data.component.html',
    styleUrls: ['./group-data.component.scss'],
})
export class GroupDataComponent {

    // view option for displaing group data
    view_OptionList: Array<string> = ['list', 'tree'];
    view_Option = 'list';

    constructor() { }
}
