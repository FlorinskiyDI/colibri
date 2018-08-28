import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { UserManageRoutingModule } from './user-manage.routes';
/* component */ import { UserManageComponent } from './user-manage.component';

@NgModule({
    imports: [
        UserManageRoutingModule,
        SharedModule
    ],
    declarations: [
        UserManageComponent
    ],
    providers: [
    ]
})

export class UserManageModule { }
