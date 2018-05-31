import { Routes, RouterModule } from '@angular/router';

/* component */ import { SurveyComponent } from './survey.component';
// /* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';

/* component */ import { BuilderComponent } from './builder/builder.component';

const routes: Routes = [
    {
        path: 'surveys',
        component: SurveyComponent,
        data: { breadcrumb: 'Surveys managment' },
        children: [
            // {
            //     path: ':id',
            //     component: SurveyBuilderComponent,
            //     data: { breadcrumb: 'builder' },
            // },
            {
                path: 'builder',
                component: BuilderComponent,
            },
            {
                path: 'builder/:id',
                component: BuilderComponent,
            },

        ]
    }
];

export const SurveyRoutingModule = RouterModule.forChild(routes);
