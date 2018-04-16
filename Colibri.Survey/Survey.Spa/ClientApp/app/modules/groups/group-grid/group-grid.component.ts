import { Component, ViewEncapsulation } from '@angular/core';
import { TreeNode } from 'primeng/api';

@Component({
    selector: 'group-grid-cmp',
    templateUrl: './group-grid.component.html',
    styleUrls: ['./group-grid.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GroupGridComponent {


    groups: TreeNode[];
    selectedGroups: TreeNode[];

    cars: any[] = [];

    constructor() { }

    ngOnInit() {

        this.groups = [
            {
                'data': {
                    'name': 'name1 test test test test test test test test',
                    'description': 'description1'
                },
                'leaf': false
            },
            {
                'data': {
                    'name': 'name2 test test test test test test test test',
                    'description': 'description2'
                },
                'leaf': false
            }
        ];
    }

    selectedGroup(data: any) {
        console.log(data);
    }

    loadNode(event: any) {
        if (event.node) {
            event.node.children = [
                {
                    'data': {
                        'name': 'name1 test test test test test test test test test test test test test test test test',
                        'description': 'description1'
                    },
                    'leaf': false
                },
                {
                    'data': {
                        'name': 'name2 test test test test test test test test test test test test test test test test',
                        'description': 'description2'
                    },
                    'leaf': false
                }
            ];
        }
    }
}
