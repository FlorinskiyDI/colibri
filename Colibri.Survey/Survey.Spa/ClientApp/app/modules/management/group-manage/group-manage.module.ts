import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupManageRoutingModule } from './group-manage.routes';
/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';
/* component */ import { GroupDialogCreateComponent } from './group-dialog-create/group-dialog-create.component';
/* component */ import { FormGroupCreateComponent } from './group-dialog-create/form-group-create/form-group-create.component';


@NgModule({
    imports: [
        GroupManageRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupManageComponent,
        GroupGridComponent,
        GroupDialogCreateComponent,
        FormGroupCreateComponent
    ],
    providers: [
    ]
})

export class GroupManageModule { }
