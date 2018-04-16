import { Component } from '@angular/core';
import { TreeNode } from 'primeng/api';

@Component({
    selector: 'group-grid-cmp',
    templateUrl: './group-grid.component.html',
    styleUrls: ['./group-grid.component.scss']
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
                    'name': 'name1',
                    'description': 'description1'
                },
                'leaf': false
            },
            {
                'data': {
                    'name': 'name2',
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
                        'name': 'Lazy Folder 0',
                        'size': '75kb',
                        'type': 'Folder'
                    },
                    'leaf': false
                },
                {
                    'data': {
                        'name': 'Lazy Folder 1',
                        'size': '150kb',
                        'type': 'Folder'
                    },
                    'leaf': false
                }
            ];
        }
    }
}
