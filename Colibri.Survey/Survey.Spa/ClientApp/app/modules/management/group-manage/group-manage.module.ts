import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupManageRoutingModule } from './group-manage.routes';
/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';



@NgModule({
    imports: [
        GroupManageRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupManageComponent,
        GroupGridComponent
    ],
    providers: [
    ]
})

export class GroupManageModule { }
