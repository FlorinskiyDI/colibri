import { Component } from '@angular/core';

@Component({
    selector: 'group-grid-cmp',
    templateUrl: './group-grid.component.html',
})
export class GroupGridComponent {

    cars: any[] = [];

    constructor() {
        this.cars = [
            { 'brand': 'VW', 'year': 2012, 'color': 'Orange', 'vin': 'dsad231ff' },
            { 'brand': 'Audi', 'year': 2011, 'color': 'Black', 'vin': 'gwregre345' },
            { 'brand': 'Renault', 'year': 2005, 'color': 'Gray', 'vin': 'h354htr' },
            { 'brand': 'BMW', 'year': 2003, 'color': 'Blue', 'vin': 'j6w54qgh' }
        ];
    }
}
