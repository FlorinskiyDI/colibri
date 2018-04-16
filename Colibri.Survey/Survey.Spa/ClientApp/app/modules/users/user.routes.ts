import { Routes, RouterModule } from '@angular/router';

/* component */ import { UserComponent } from './user.component';
/* component */ import { UserGridComponent } from './user-grid/user-grid.component';

const routes: Routes = [
    {
        path: 'users',
        component: UserComponent,
        data: { breadcrumb: 'User managment' },
        children: [
            {
                path: '',
                component: UserGridComponent,
                data: { breadcrumb: 'Grid' },
            },
        ]
    }
];

export const UserRoutingModule = RouterModule.forChild(routes);
