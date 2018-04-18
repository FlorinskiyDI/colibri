import { Component, ViewEncapsulation } from '@angular/core';
import { TreeDragDropService } from 'primeng/components/common/api';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';

@Component({
    selector: 'group-tree-cmp',
    templateUrl: './group-tree.component.html',
    styleUrls: ['./group-tree.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers: [TreeDragDropService]
})
export class GroupTreeComponent {

    dialogGroupCreateConfig: DialogDataModel<any>;
    selectedGroup: any;
    treeItems: any[] = [];
    treeloading = false;

    constructor() {
        const that = this;
        this._stub1().then(function (data: any) {
            that.treeItems = data;
            that.treeloading = false;
        });
    }

    public createGroup() { this.dialogGroupCreateOpen(); }
    public removeGroup(data: any) { console.log(`removeGroup - ${data.id}`); }
    public selectGroup(data: any) { console.log(`selectGroup - ${data.node && data.node.data && data.node.data.id}`); }
    public searchGroups(data: any) {
        const that = this;
        if (data === '') {
            this._stub1().then(function (value: any) {
                that.treeItems = value;
                that.selectedGroup = null;
                that.treeloading = false;
            });
        } else {
            this._stub2().then(function (value: any) {
                that.treeItems = value;
                that.selectedGroup = null;
                that.treeloading = false;
            });
        }
    }

    public dialogGroupCreateOpen() { this.dialogGroupCreateConfig = new DialogDataModel<any>(true); }
    public dialogGroupCreateOnChange() { console.log('dialogGroupCreateOnChange'); }
    public dialogGroupCreateOnCancel() { console.log('dialogGroupCreateOnCancel'); }
    public dialogGroupCreateOnHide() { console.log('dialogGroupCreateOnHide'); }


    loadNode(event: any) {
        this.treeloading = true;
        const that = this;
        this._stub1().then(function (value: any) {
            event.node.children = value;
            that.treeloading = false;
        });
    }

    _stub1() {
        this.treeloading = true;
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { 'label': 'Test log name for node 1', 'data': { 'id': 1 }, 'leaf': false },
                    { 'label': 'Test log name for node 2', 'data': { 'id': 2 }, 'leaf': false },
                    { 'label': 'Test log name for node 3', 'data': { 'id': 3 }, 'leaf': false }
                ];
                resolve(data);
            }, 1000);
        });
    }

    _stub2() {
        this.treeloading = true;
        return new Promise(function (resolve, reject) {
            window.setTimeout(function () {
                const data = [
                    { 'label': 'Test log name for node 1', 'data': { 'id': 1 } },
                    { 'label': 'Test log name for node 2', 'data': { 'id': 2 } },
                ];
                resolve(data);
            }, 1000);
        });
    }

}
