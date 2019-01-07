import { Routes, RouterModule } from '@angular/router';

/* component */ import { UserManageComponent } from './user-manage.component';
// /* component */ import { UserGridComponent } from './user-grid/user-grid.component';

const routes: Routes = [
    {
        path: 'users',
        component: UserManageComponent,
        data: { breadcrumb: 'User managment' },
        children: [
            {
                path: '',
                // component: UserGridComponent,
                data: { breadcrumb: 'users' },
            },
        ]
    }
];

export const UserManageRoutingModule = RouterModule.forChild(routes);
