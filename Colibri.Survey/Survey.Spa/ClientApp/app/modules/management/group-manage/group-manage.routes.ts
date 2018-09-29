import { Routes, RouterModule } from '@angular/router';

/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';
/* component */ import { GroupViewComponent } from './group-view/group-view.component';
/* component */ import { GroupDetailComponent } from './group-detail/group-detail.component';

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
            {
                path: ':id',
                component: GroupViewComponent,
                data: { breadcrumb: '' },
                children: [
                    {
                        path: 'detail',
                        component: GroupDetailComponent,
                        data: { breadcrumb: '' },
                    },
                ]
            },

        ]
    }
];

export const GroupManageRoutingModule = RouterModule.forChild(routes);
