import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { UserManageRoutingModule } from './user-manage.routes';
/* component */ import { UserManageComponent } from './user-manage.component';
/* component */ import { UserGridComponent } from './user-grid/user-grid.component';
/* pipe */ import { UserStatusPipe } from './user-grid/user-status.pipe';

@NgModule({
    imports: [
        UserManageRoutingModule,
        SharedModule
    ],
    declarations: [
        // components
        UserManageComponent,
        UserGridComponent,
        // pipes
        UserStatusPipe
    ],
    providers: [
    ]
})

export class UserManageModule { }
