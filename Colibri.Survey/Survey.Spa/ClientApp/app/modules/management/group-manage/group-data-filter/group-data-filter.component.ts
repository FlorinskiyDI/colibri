import { Component, ViewChild, ElementRef } from '@angular/core';
import { COMMA } from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent, MatChipInputEvent, MatAutocomplete } from '@angular/material';
import { FormControl } from '@angular/forms';

/* model-api */ import { PageFilterStatement } from 'shared/models/entities/api/page-search-entry.api.model';



@Component({
    selector: 'cmp-group-data-filter',
    templateUrl: './group-data-filter.component.html',
    styleUrls: ['./group-data-filter.component.scss']
})
export class GroupDataFilterComponent {
    // ///////////
    // ///////////
    // visible = true;
    chipSelectable = true;
    chipRemovable = true;
    chipAddOnBlur = true;
    chipSeparatorKeysCodes: number[] = [COMMA];
    chipFruitCtrl = new FormControl();
    filteredItems: any[];
    chipItems: FilterItem[] = [];
    // fruits: string[] = [];
    pageColumnNames: string[] = ['Name', 'Description'];

    @ViewChild('chipInput') chipInput: ElementRef<HTMLInputElement>;
    @ViewChild('auto') matAutocomplete: MatAutocomplete;
    // ///////////
    // ///////////


    constructor() {
        this.chipFruitCtrl.valueChanges.subscribe((data: any) => {
            if (data === '') {
                this.filterItem = new FilterItem();
            }
            if (this.filterItem.filterStatement.propertyName) {
                data = data.split(':').pop();
            }
            this.filteredItems = data ? this._filter(data) : this.pageColumnNames.slice();
            // if (this.chipItems.length > 0) {
            //     this.filteredItems.unshift('OR');
            // }
        });
    }

    chipOnEnter(data: any) {
    }

    add(event: MatChipInputEvent): void {
        // debugger
        console.log(event);
        // debugger
        // Add item only when MatAutocomplete is not open
        // To make sure this does not conflict with OptionSelected Event
        if (!this.matAutocomplete.isOpen) {
            console.log(this.matAutocomplete.isOpen);
            const input = event.input;
            let value = event.value;

            if ((value || '').trim()) {
                if (this.filterItem.filterStatement && this.filterItem.filterStatement.propertyName == null) {
                    this.filterItem.filterStatement.propertyName = value;
                    const lastChar = value.substr(value.length - 1);
                    if (lastChar && lastChar !== ':') {
                        this.chipInput.nativeElement.value = value + ':';
                    }

                } else {
                    value = value.split(':').pop();
                    if (value && value !== '') {
                        this.chipInput.nativeElement.value = null;
                        this.filterItem.filterStatement.value = value.split(':').pop();
                        this.chipItems.push({
                            label: this.filterItem.filterStatement.propertyName + ':' + this.filterItem.filterStatement.value,
                            filterStatement: Object.assign({}, this.filterItem.filterStatement)
                        });
                        this.filterItem = new FilterItem();
                    }


                }
            }
            this.chipFruitCtrl.setValue(null);
            // Add our fruit
            // if ((value || '').trim()) {
            //     this.chipItems.push({ label: value.trim() });
            // }

            // // Reset the input value
            // if (input) {
            //     input.value = '';
            // }
        }
    }

    remove(item: any): void {

        const index = this.chipItems.indexOf(item);
        this.filterItem = new FilterItem();
        this.chipFruitCtrl.setValue(null);
        if (index >= 0) {
            this.chipItems.splice(index, 1);
        }
    }

    filterItem = new FilterItem();
    selected(event: MatAutocompleteSelectedEvent): void {

        if (this.filterItem.filterStatement && this.filterItem.filterStatement.propertyName == null) {
            this.filterItem.filterStatement.propertyName = event.option.viewValue;
            this.chipInput.nativeElement.value = event.option.viewValue + ':';
        } else {
            this.chipInput.nativeElement.value = null;
            this.filterItem.filterStatement.value = event.option.viewValue;
            this.chipItems.push({
                label: this.filterItem.filterStatement.propertyName + ':' + this.filterItem.filterStatement.value,
                filterStatement: Object.assign({}, this.filterItem.filterStatement)
            });
            this.filterItem = new FilterItem();
            this.chipFruitCtrl.setValue(null);
        }

    }

    private _filter(value: string): string[] {
        const filterValue = value.toLowerCase();

        return this.pageColumnNames.filter(data => data.toLowerCase().indexOf(filterValue) === 0);
    }
}


export class FilterItem {
    public label: string;
    public filterStatement?: PageFilterStatement = new PageFilterStatement();
}

