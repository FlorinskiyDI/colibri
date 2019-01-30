import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { IdentityServerRoutingModule } from './identity-server.routes';
/* component */ import { GroupComponent } from './containers/group/group.component';
/* component */ import { GroupManageComponent } from './containers/group-manage/group-manage.component';
/* component */ import { GroupDataTreeComponent } from './components/group/group-data-tree/group-data-tree.component';
/* component */ import { GroupDataGridComponent } from './components/group/group-data-grid/group-data-grid.component';
/* component */ import { GroupOverviewComponent } from './containers/group-overview/group-overview.component';
/* component */ import { GroupOverviewMainComponent } from './containers/group-overview-main/group-overview-main.component';
/* component */ import { GroupDialogCreateComponent } from './components/group/group-dialog-create/group-dialog-create.component';
/* component */ import { GroupFormCreateComponent } from './components/group/group-form-create/group-form-create.component';
/* component */ import { GroupFormUpdateComponent } from './components/group/group-form-update/group-form-update.component';
/* component */ import { GroupCreateComponent } from './containers/group-create/group-create.component';
/* component */ import { SystemConfigurationComponent } from './containers/system-configuration/system-configuration.component';
/* component */ import { AccessManageComponent } from './containers/access-manage/access-manage.component';
/* component */ import { MemberDataGridComponent } from './components/member/member-data-grid/member-data-grid.component';
/* component */ import { MemberDialogCreateComponent } from './components/member/member-dialog-create/member-dialog-create.component';
/* component */ import { UserComponent } from './containers/user/user.component';
/* component */ import { UserManageComponent } from './containers/user-manage/user-manage.component';
/* component */ import { UserProfileComponent } from './containers/user-profile/user-profile.component';
/* component */ import { UserDataGridComponent } from './components/user/user-data-grid/user-data-grid.component';
/* component */ import { UserDialogOverviewComponent } from './components/user/user-dialog-overview/user-dialog-overview.component';
/* component */ import { UserDialogInviteComponent } from './components/user/user-dialog-invite/user-dialog-invite.component';
/* component */ import { HomeComponent } from './containers/home/home.component';
/* component */ import { DashboardComponent } from './components/dashboard/dashboard.component';

/* pipe */ import { UserStatusPipe } from './common/pipes/user-status.pipe';
/* service */ import { UserService } from './common/services/user.service';

@NgModule({
    imports: [
        SharedModule,
        IdentityServerRoutingModule
    ],
    declarations: [
        // components
        GroupComponent,
        GroupOverviewComponent,
        GroupOverviewMainComponent,
        GroupManageComponent,
        GroupDataTreeComponent,
        GroupDataGridComponent,
        GroupDialogCreateComponent,
        GroupFormCreateComponent,
        GroupFormUpdateComponent,
        GroupCreateComponent,
        UserComponent,
        UserManageComponent,
        UserProfileComponent,
        UserDataGridComponent,
        UserDialogOverviewComponent,
        UserDialogInviteComponent,
        AccessManageComponent,
        MemberDataGridComponent,
        MemberDialogCreateComponent,
        SystemConfigurationComponent,
        HomeComponent,
        DashboardComponent,
        // pipes
        UserStatusPipe
    ],
    providers: [
        UserService
    ]
})

export class IdentityServerModule { }
