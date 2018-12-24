import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { ManagementSharedModule } from '../../management-shared.module';
/* module */ import { UserManageRoutingModule } from './user-manage.routes';
/* component */ import { UserManageComponent } from './user-manage.component';
/* component */ import { UserGridComponent } from './user-grid/user-grid.component';
/* component */ import { UserDialogDetailsComponent } from './user-dialog-details/user-dialog-details.component';

@NgModule({
    imports: [
        SharedModule,
        ManagementSharedModule,
        UserManageRoutingModule
    ],
    declarations: [
        // components
        UserManageComponent,
        UserGridComponent,
        UserDialogDetailsComponent
    ],
    providers: [
    ]
})

export class UserManageModule { }
