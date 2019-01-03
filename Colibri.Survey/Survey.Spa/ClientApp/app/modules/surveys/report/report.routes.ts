import { Routes, RouterModule } from '@angular/router';

/* component */ import { ReportComponent } from './report.component';
/* component */ import { TableReportComponent } from './tableReport/tableReport.component';

const routes: Routes = [
    {
        path: '',
        component: ReportComponent,
        children: [
            {
                path: ':id',
                component: TableReportComponent
            }
        ]
    }
];

export const ReportRoutingModule = RouterModule.forChild(routes);
