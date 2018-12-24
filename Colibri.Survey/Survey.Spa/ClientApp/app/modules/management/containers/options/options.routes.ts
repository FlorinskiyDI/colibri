import { Routes, RouterModule } from '@angular/router';

/* component */ import { OptionsComponent } from './options.component';

const routes: Routes = [
    {
        path: 'options',
        component: OptionsComponent,
        // data: { breadcrumb: '' }
    }
];

export const OptionsRoutingModule = RouterModule.forChild(routes);
