import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { UserRoutingModule } from './user.routes';
/* component */ import { UserComponent } from './user.component';
/* component */ import { UserGridComponent } from './user-grid/user-grid.component';


@NgModule({
    imports: [
        UserRoutingModule,
        SharedModule
    ],
    declarations: [
        UserComponent,
        UserGridComponent
    ],
    providers: [ ]
})
export class UserModule { }
