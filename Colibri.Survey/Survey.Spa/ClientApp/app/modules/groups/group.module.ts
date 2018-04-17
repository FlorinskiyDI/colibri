import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupRoutingModule } from './group.routes';
/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupTreeComponent } from './group-tree/group-tree.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';
/* component */ import { GroupMemberGridComponent } from './group-member-grid/group-member-grid.component';


@NgModule({
    imports: [
        GroupRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupComponent,
        GroupTreeComponent,
        GroupGridComponent,
        GroupMemberGridComponent
    ],
    providers: [ ]
})
export class GroupModule { }
