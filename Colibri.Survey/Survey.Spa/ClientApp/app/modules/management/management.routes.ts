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
                loadChildren: 'modules/management/containers/options/options.module#OptionsModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/containers/profile/profile.module#ProfileModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/containers/group-manage/group-manage.module#GroupManageModule',
            },
            {
                path: '',
                loadChildren: 'modules/management/containers/user-manage/user-manage.module#UserManageModule',
            },
        ]
    }
];

export const ManagementRoutingModule = RouterModule.forChild(routes);
