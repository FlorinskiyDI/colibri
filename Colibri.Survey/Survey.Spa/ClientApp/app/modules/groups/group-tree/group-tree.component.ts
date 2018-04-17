import { Component, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'group-tree-cmp',
    templateUrl: './group-tree.component.html',
    styleUrls: ['./group-tree.component.scss'],
    encapsulation: ViewEncapsulation.None,
})
export class GroupTreeComponent {

    treeItems: any[] = [];

    constructor() {
        this.treeItems = [
            { 'label': 'Test log name for node 1', 'data': 'Test node 1', 'leaf': false },
            { 'label': 'Test log name for node 2', 'data': 'Test node 2', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 3', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 4', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 5', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 6', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 7', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 8', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 9', 'leaf': false },
        ];
    }

    loadNode(event: any) {
        event.node.children = [
            { 'label': 'Test log name for node 1', 'data': 'Test node 1', 'leaf': false },
            { 'label': 'Test log name for node 2', 'data': 'Test node 2', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 3', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 4', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 5', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 6', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 7', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 8', 'leaf': false },
            { 'label': 'Test log name for node 1', 'data': 'Test node 9', 'leaf': false },
        ];

    }
}
