import { Routes, RouterModule } from '@angular/router';

/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupManageComponent } from './group-manage/group-manage.component';

const routes: Routes = [
    {
        path: 'groups',
        component: GroupComponent,
        data: { breadcrumb: 'Group managment' },
        children: [
            {
                path: '',
                component: GroupManageComponent,
                data: { breadcrumb: 'Grid' },
            },
        ]
    }
];

export const GroupRoutingModule = RouterModule.forChild(routes);
