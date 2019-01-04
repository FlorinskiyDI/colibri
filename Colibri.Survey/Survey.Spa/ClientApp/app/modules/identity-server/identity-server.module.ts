import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { IdentityServerRoutingModule } from './identity-server.routes';
/* component */ import { GroupDetailsComponent } from './containers/group-details/group-details.component';
/* component */ import { GroupManageComponent } from './containers/group-manage/group-manage.component';
/* component */ import { SystemConfigurationComponent } from './containers/system-configuration/system-configuration.component';
/* component */ import { UserDialogDetailsComponent } from './containers/user-dialog-details/user-dialog-details.component';
/* component */ import { UserManageComponent } from './containers/user-manage/user-manage.component';
/* component */ import { UserProfileComponent } from './containers/user-profile/user-profile.component';

@NgModule({
    imports: [
        SharedModule,
        IdentityServerRoutingModule
    ],
    declarations: [
        // components
        GroupDetailsComponent,
        GroupManageComponent,
        SystemConfigurationComponent,
        UserDialogDetailsComponent,
        UserManageComponent,
        UserProfileComponent
        // pipes
        // UserStatusPipe
    ],
    providers: [
        // UserService
    ]
})

export class IdentityServerModule { }
