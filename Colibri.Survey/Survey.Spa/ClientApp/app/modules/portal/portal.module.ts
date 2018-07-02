import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { PortalRoutingModule } from './portal.routes';

/* component */ import { PortalComponent } from './portal.component';

/* component */ import { MainPortalComponent } from './main-portal/main-portal.component';
/* component */ import { SurveyViewerComponent } from './survey-viewer/survey-viewer.component';
/* component */ import { SurveyViewerFormComponent } from './survey-viewer/survey-viewer-form/survey-viewer-form.component';
/* component */ import { SurveyViewerQuestionComponent } from './survey-viewer/survey-viewer-form/survey-viewer-question/survey-viewer-question.component';
import {  MdlCheckboxModule, MdlRadioModule } from '@angular-mdl/core';


import { ParticlesModule } from 'angular-particle';
import {StickyModule} from 'ng2-sticky-kit';

@NgModule({
    imports: [
        PortalRoutingModule,
        SharedModule,
        ParticlesModule,
        StickyModule,
        MdlCheckboxModule,
        MdlRadioModule
    ],
    exports: [
        MdlCheckboxModule,
        MdlRadioModule
    ],
    declarations: [
        PortalComponent,
        MainPortalComponent,
        SurveyViewerComponent,
        SurveyViewerFormComponent,
        SurveyViewerQuestionComponent
    ],
    entryComponents: [

      ],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class PortalModule { }
