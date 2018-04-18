import { Component, ViewEncapsulation } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';

@Component({
    selector: 'group-tree-cmp',
    templateUrl: './group-tree.component.html',
    styleUrls: ['./group-tree.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [ TreeDragDropService ]
})
export class GroupTreeComponent {

    treeItems: any[] = [];
    loading = false;

    constructor(
    ) {
        this.treeItems = [
            { 'label': 'Test log name for node 1', 'data': 'Test node 1', 'leaf': false },
            { 'label': 'Test log name for node 2', 'data': 'Test node 2', 'leaf': false },
            { 'label': 'Test log name for node 3', 'data': 'Test node 3', 'leaf': false },
            { 'label': 'Test log name for node 4', 'data': 'Test node 4', 'leaf': false },
        ];
    }



    loadNode(event: any) {
        this.loading = true;
        setTimeout(
            function (mythis: any) {
                mythis.loading = false;
                event.node.children = [
                    { 'label': 'Test log name for node 1', 'data': 'Test node 1', 'leaf': false },
                    { 'label': 'Test log name for node 2', 'data': 'Test node 2', 'leaf': false },
                    { 'label': 'Test log name for node 3', 'data': 'Test node 3', 'leaf': false },
                    { 'label': 'Test log name for node 4', 'data': 'Test node 4', 'leaf': false },
                ];
            },
            1000,
            this
        );
    }
}
