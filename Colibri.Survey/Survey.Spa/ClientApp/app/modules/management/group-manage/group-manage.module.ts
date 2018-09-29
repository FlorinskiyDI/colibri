import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupManageRoutingModule } from './group-manage.routes';
/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';
/* component */ import { GroupViewComponent } from './group-view/group-view.component';
/* component */ import { GroupDetailComponent } from './group-detail/group-detail.component';
/* component */ import { GroupDialogCreateComponent } from './group-dialog-create/group-dialog-create.component';
/* component */ import { GroupFormCreateComponent } from './group-dialog-create/group-form-create/group-form-create.component';


@NgModule({
    imports: [
        GroupManageRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupManageComponent,
        GroupGridComponent,
        GroupDialogCreateComponent,
        GroupFormCreateComponent,
        GroupViewComponent,
        GroupDetailComponent
    ],
    providers: [
    ]
})

export class GroupManageModule { }
