import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupRoutingModule } from './group.routes';
/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupTreeComponent } from './group-tree/group-tree.component';
/* component */ import { GroupManageComponent } from './group-manage/group-manage.component';
/* component */ import { GroupMemberGridComponent } from './group-member-grid/group-member-grid.component';
/* component */ import { DialogGroupCreateComponent } from './dialog-group-create/dialog-group-create.component';
/* component */ import { DialogGroupMemberAddComponent } from './dialog-group-member-add/dialog-group-member-add.component';


@NgModule({
    imports: [
        GroupRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupComponent,
        GroupTreeComponent,
        GroupManageComponent,
        GroupMemberGridComponent,
        DialogGroupCreateComponent,
        DialogGroupMemberAddComponent
    ],
    providers: [ ]
})
export class GroupModule { }
