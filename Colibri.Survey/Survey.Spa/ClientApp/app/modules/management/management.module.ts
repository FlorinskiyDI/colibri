import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { ManagementRoutingModule } from './management.routes';
/* component */ import { ManagementComponent } from './management.component';

@NgModule({
    imports: [
        ManagementRoutingModule,
        SharedModule
    ],
    declarations: [
        ManagementComponent
    ],
    providers: [ ]
})

export class ManagementModule { }
