import { Routes, RouterModule } from '@angular/router';

/* component */ import { PortalComponent } from './portal.component';
// /* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';
// /* component */ import { SurveyGridComponent } from './survey-grid/survey-grid.component';
// /* component */ import { BuilderComponent } from './builder/builder.component';

/* component */ import { MainPortalComponent } from './main-portal/main-portal.component';
/* component */ import { SurveyViewerComponent } from './survey-viewer/survey-viewer.component';

const routes: Routes = [
    {
        path: 'portal',
        component: PortalComponent,
        children: [
            {
                path: '',
                component: MainPortalComponent,
            },
            {
                path: '/:id',
                component: SurveyViewerComponent,
            },
        ]
    }
];

export const PortalRoutingModule = RouterModule.forChild(routes);
