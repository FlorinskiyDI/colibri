import { Component, ViewEncapsulation } from '@angular/core';
import { TreeNode } from 'primeng/api';

@Component({
    selector: 'group-manage-cmp',
    templateUrl: './group-manage.component.html',
    styleUrls: ['./group-manage.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GroupManageComponent {


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
