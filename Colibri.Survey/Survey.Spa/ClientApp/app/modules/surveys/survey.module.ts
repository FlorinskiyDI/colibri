import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { SurveyRoutingModule } from './survey.routes';
/* component */ import { SurveyComponent } from './survey.component';
/* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';

import { DndModule } from 'ng2-dnd';
@NgModule({
    imports: [
        SurveyRoutingModule,
        SharedModule,
        DndModule.forRoot(),
    ],
    declarations: [
        SurveyComponent,
        SurveyBuilderComponent
        // GroupGridComponent
    ],
    providers: [ ]
})
export class SurveyModule { }
