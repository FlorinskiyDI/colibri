import { Routes, RouterModule } from '@angular/router';

/* component */ import { DashboardComponent } from './dashboard.component';

const routes: Routes = [
    {
        path: 'dashboard',
        component: DashboardComponent,
        // data: { breadcrumb: '' }
    }
];

export const DashboardRoutingModule = RouterModule.forChild(routes);
