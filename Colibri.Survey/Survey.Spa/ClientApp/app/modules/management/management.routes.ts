import { Routes, RouterModule } from '@angular/router';

/* component */ import { ManagementComponent } from './management.component';

const routes: Routes = [
    {
        path: 'manage',
        component: ManagementComponent,
        // data: { breadcrumb: '' },
        children: [
            {
                path: '',
                loadChildren: 'modules/management/options/options.module#OptionsModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/profile/profile.module#ProfileModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/groups/group.module#GroupModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/users/user.module#UserModule',
            },
        ]
    }
];

export const ManagementRoutingModule = RouterModule.forChild(routes);
