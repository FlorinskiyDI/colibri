import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { SurveyRoutingModule } from './survey.routes';
/* component */ import { SurveyComponent } from './survey.component';
/* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';
/* component */ import { SurveyFormBuilderComponent } from './survey-builder/survey-form-builder/survey-form-builder.component';
/* component */ import { SurveyFormQuestionComponent } from './survey-builder/survey-form-builder/survey-form-question/survey-form-question.component';



import { DndModule } from 'ng2-dnd';
@NgModule({
    imports: [
        SurveyRoutingModule,
        SharedModule,
        DndModule.forRoot(),
    ],
    declarations: [
        SurveyComponent,
        SurveyBuilderComponent,
        SurveyFormBuilderComponent,
        SurveyFormQuestionComponent
        // GroupGridComponent
    ],
    providers: [ ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SurveyModule { }
