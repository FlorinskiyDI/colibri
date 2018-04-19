import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupRoutingModule } from './group.routes';
/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupTreeComponent } from './group-tree/group-tree.component';
/* component */ import { GroupManageComponent } from './group-manage/group-manage.component';
/* component */ import { GroupMemberGridComponent } from './group-member-grid/group-member-grid.component';
/* component */ import { DialogGroupCreateComponent } from './dialog-group-create/dialog-group-create.component';
/* component */ import { DialogGroupMemberAddComponent } from './dialog-group-member-add/dialog-group-member-add.component';
/* component */ import { DialogGroupMemberDetailComponent } from './dialog-group-member-detail/dialog-group-member-detail.component';
/* component */ import { FormGroupMemberAddComponent } from './dialog-group-member-add/form-group-member-add/form-group-member-add.component';
/* component */ import { FormGroupCreateComponent } from './dialog-group-create/form-group-create/form-group-create.component';
/* component */ import { GroupInfoComponent } from './group-info/group-info.component';
/* component */ import { FormGroupUpdateComponent } from './group-info/form-group-update/form-group-update.component';
/* service */ import { GroupManageTransferService } from './group-manage/group-manage.transfer.service';



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
        DialogGroupMemberAddComponent,
        DialogGroupMemberDetailComponent,
        FormGroupCreateComponent,
        FormGroupMemberAddComponent,
        GroupInfoComponent,
        FormGroupUpdateComponent
    ],
    providers: [
        GroupManageTransferService
    ]
})
export class GroupModule { }
