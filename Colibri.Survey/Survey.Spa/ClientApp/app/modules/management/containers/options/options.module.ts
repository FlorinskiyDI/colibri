import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { OptionsRoutingModule } from './options.routes';
/* component */ import { OptionsComponent } from './options.component';

@NgModule({
    imports: [
        OptionsRoutingModule,
        SharedModule
    ],
    declarations: [
        OptionsComponent
    ],
    providers: [ ]
})

export class OptionsModule { }
