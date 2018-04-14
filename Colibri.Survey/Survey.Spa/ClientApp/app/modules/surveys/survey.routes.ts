import { Routes, RouterModule } from '@angular/router';

/* component */ import { SurveyComponent } from './survey.component';
/* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';

const routes: Routes = [
    {
        path: 'surveys',
        component: SurveyComponent,
        data: { breadcrumb: 'Surveys managment' },
        children: [
            {
                path: ':id',
                component: SurveyBuilderComponent,
                data: { breadcrumb: 'builder' },
            },
        ]
    }
];

export const SurveyRoutingModule = RouterModule.forChild(routes);
