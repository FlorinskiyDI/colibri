import { Routes, RouterModule } from '@angular/router';

/* component */ import { UserManageComponent } from './user-manage.component';

const routes: Routes = [
    {
        path: 'groups',
        component: UserManageComponent,
        data: { breadcrumb: 'Group managment' },
        // children: [
        //     {
        //         path: '',
        //         component: GroupManageComponent,
        //         data: { breadcrumb: 'Grid' },
        //     },
        // ]
    }
];

export const UserManageRoutingModule = RouterModule.forChild(routes);
