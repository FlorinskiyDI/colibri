import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
// /* module */ import { ManagementSharedModule } from '../../management-shared.module';
/* module */ import { GroupManageRoutingModule } from './group-manage.routes';
/* component */ import { GroupManageComponent } from './group-manage.component';
// /* component */ import { GroupDataTreeComponent } from './group-data-tree/group-data-tree.component';
// /* component */ import { GroupDataGridComponent } from './group-data-grid/group-data-grid.component';
/* component */ import { GroupViewComponent } from './group-view/group-view.component';
/* component */ import { GroupViewDetailComponent } from './group-view-detail/group-view-detail.component';
// /* component */ import { GroupDialogCreateComponent } from './group-dialog-create/group-dialog-create.component';
/* component */ import { GroupDataComponent } from './group-data/group-data.component';
// /* component */ import { MemberGridComponent } from './member-grid/member-grid.component';
// /* component */ import { MemberDialogCreateComponent } from './member-dialog-create/member-dialog-create.component';
// /* pipe */ import { UserStatusPipe } from '../../common/pipes/user-status.pipe';
// /* service */ import { UserService } from '../../common/services/user.service';

@NgModule({
    imports: [
        SharedModule,
        // ManagementSharedModule,
        GroupManageRoutingModule
    ],
    declarations: [
        // components
        GroupDataComponent,
        GroupManageComponent,
        // GroupDataTreeComponent,
        // GroupDataGridComponent,
        // GroupDialogCreateComponent,
        GroupViewComponent,
        GroupViewDetailComponent,
        // MemberGridComponent,
        // MemberDialogCreateComponent,
        // pipes
        // UserStatusPipe
    ],
    providers: [
        // UserService
    ]
})

export class GroupManageModule { }
