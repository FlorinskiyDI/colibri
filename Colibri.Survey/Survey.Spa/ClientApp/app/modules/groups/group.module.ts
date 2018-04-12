import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { GroupRoutingModule } from './group.routes';
/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';


@NgModule({
    imports: [
        GroupRoutingModule,
        SharedModule
    ],
    declarations: [
        GroupComponent,
        GroupGridComponent
    ],
    providers: [ ]
})
export class GroupModule { }
