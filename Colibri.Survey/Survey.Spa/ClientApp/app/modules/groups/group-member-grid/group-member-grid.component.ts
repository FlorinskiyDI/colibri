import { Component } from '@angular/core';

@Component({
    selector: 'group-member-grid-cmp',
    templateUrl: './group-member-grid.component.html'
})
export class GroupMemberGridComponent {

    gridItems: any[] = [];
    gridCols: any[] = [];

    constructor() {
        this.gridItems = [
            { 'brand': 'VW', 'year': 2012, 'color': 'Orange', 'vin': 'dsad231ff' },
            { 'brand': 'Audi', 'year': 2011, 'color': 'Black', 'vin': 'gwregre345' },
            { 'brand': 'Renault', 'year': 2005, 'color': 'Gray', 'vin': 'h354htr' },
            { 'brand': 'BMW', 'year': 2003, 'color': 'Blue', 'vin': 'j6w54qgh' }
        ];

        this.gridCols = [
            { field: 'brand', header: 'Brand' },
            { field: 'year', header: 'Year' },
            { field: 'color', header: 'Color' },
            { field: 'vin', header: 'Vin' }
        ];
    }
}
