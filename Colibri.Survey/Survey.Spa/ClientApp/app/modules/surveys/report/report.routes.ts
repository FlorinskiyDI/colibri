import { Routes, RouterModule } from '@angular/router';

/* component */ import { ReportComponent } from './report.component';
// /* component */ import { SurveyBuilderComponent } from './survey-builder/survey-builder.component';
/* component */ import { TableReportComponent } from './tableReport/tableReport.component';
// /* component */ import { BuilderComponent } from './builder/builder.component';

const routes: Routes = [
    {
        path: '',
        component: ReportComponent,
        // data: { breadcrumb: 'Surveys managment' },
        children: [
            {
                path: ':id',
                component: TableReportComponent,
                // data: { breadcrumb: 'builder' },
            },
            // {
            //     path: 'builder',
            //     component: BuilderComponent,
            // },
            // {
            //     path: 'builder/:id',
            //     component: BuilderComponent,
            // },

        ]
    }
];

export const ReportRoutingModule = RouterModule.forChild(routes);
