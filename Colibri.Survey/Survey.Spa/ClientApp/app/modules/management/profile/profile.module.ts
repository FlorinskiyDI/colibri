import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { ProfileRoutingModule } from './profile.routes';
/* component */ import { ProfileComponent } from './profile.component';

@NgModule({
    imports: [
        ProfileRoutingModule,
        SharedModule
    ],
    declarations: [
        ProfileComponent
    ],
    providers: [ ]
})

export class ProfileModule { }
