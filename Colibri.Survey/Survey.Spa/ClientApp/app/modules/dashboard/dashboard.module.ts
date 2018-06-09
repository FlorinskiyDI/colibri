import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { DashboardRoutingModule } from './dashboard.routes';
/* component */ import { DashboardComponent } from './dashboard.component';

@NgModule({
    imports: [
        DashboardRoutingModule,
        SharedModule
    ],
    declarations: [
        DashboardComponent
    ],
    providers: [ ]
})

export class DashboardModule { }
