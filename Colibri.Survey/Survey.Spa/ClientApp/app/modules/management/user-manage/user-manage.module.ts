import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { UserManageRoutingModule } from './user-manage.routes';
/* component */ import { UserManageComponent } from './user-manage.component';
/* component */ import { UserGridComponent } from './user-grid/user-grid.component';

@NgModule({
    imports: [
        UserManageRoutingModule,
        SharedModule
    ],
    declarations: [
        UserManageComponent,
        UserGridComponent
    ],
    providers: [
    ]
})

export class UserManageModule { }
