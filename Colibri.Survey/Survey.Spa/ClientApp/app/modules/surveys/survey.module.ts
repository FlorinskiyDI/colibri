import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { SurveyRoutingModule } from './survey.routes';
/* component */ import { SurveyComponent } from './survey.component';
/* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';

/* component */ import { BuilderComponent } from './builder/builder.component';
/* component */ import { FormBuilderComponent } from './builder/form-builder/form-builder.component';

/* component */ import { SurveyFormBuilderComponent } from './survey-builder/survey-form-builder/survey-form-builder.component';
/* component */ import { SurveyFormQuestionComponent } from './survey-builder/survey-form-builder/survey-form-question/survey-form-question.component';
/* component */ import { PagingComponent } from './survey-builder/paging/paging.component';
/* component */ import { QuestionOptionComponent } from './survey-builder/question-option/question-option.component';


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
        BuilderComponent,
        SurveyFormBuilderComponent,
        FormBuilderComponent,
        PagingComponent,
        SurveyFormQuestionComponent,
        QuestionOptionComponent
        // GroupGridComponent
    ],
    providers: [ ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class SurveyModule { }
