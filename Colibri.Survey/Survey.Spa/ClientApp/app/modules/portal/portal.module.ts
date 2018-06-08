import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { PortalRoutingModule } from './portal.routes';

/* component */ import { PortalComponent } from './portal.component';

/* component */ import { MainPortalComponent } from './main-portal/main-portal.component';
/* component */ import { SurveyViewerComponent } from './survey-viewer/survey-viewer.component';
// import { ParticlesModule } from 'angular-particle';

@NgModule({
    imports: [
        PortalRoutingModule,
        SharedModule,
        // ParticlesModule
    ],
    declarations: [
        PortalComponent,
        MainPortalComponent,
        SurveyViewerComponent
    ],
    entryComponents: [

      ],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PortalModule { }
