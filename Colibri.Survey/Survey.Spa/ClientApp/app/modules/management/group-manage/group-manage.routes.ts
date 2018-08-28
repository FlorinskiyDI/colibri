import { Routes, RouterModule } from '@angular/router';

/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';

const routes: Routes = [
    {
        path: 'groups',
        component: GroupManageComponent,
        data: { breadcrumb: 'Group management' },
        children: [
            {
                path: '',
                component: GroupGridComponent,
                data: { breadcrumb: '' },
            },
        ]
    }
];

export const GroupManageRoutingModule = RouterModule.forChild(routes);
