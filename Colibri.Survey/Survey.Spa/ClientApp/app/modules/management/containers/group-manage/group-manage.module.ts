import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupManageRoutingModule } from './group-manage.routes';
/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupViewComponent } from './group-view/group-view.component';
/* component */ import { GroupViewDetailComponent } from './group-view-detail/group-view-detail.component';
/* component */ import { GroupDataComponent } from './group-data/group-data.component';

@NgModule({
    imports: [
        SharedModule,
        GroupManageRoutingModule
    ],
    declarations: [
        // components
        GroupDataComponent,
        GroupManageComponent,
        GroupViewComponent,
        GroupViewDetailComponent,
        // pipes
    ],
    providers: [
    ]
})

export class GroupManageModule { }
