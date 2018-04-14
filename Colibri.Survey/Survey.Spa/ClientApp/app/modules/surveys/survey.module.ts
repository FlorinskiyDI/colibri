import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { SurveyRoutingModule } from './survey.routes';
/* component */ import { SurveyComponent } from './survey.component';
/* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';


@NgModule({
    imports: [
        SurveyRoutingModule,
        SharedModule
    ],
    declarations: [
        SurveyComponent,
        SurveyBuilderComponent
        // GroupGridComponent
    ],
    providers: [ ]
})
export class SurveyModule { }
