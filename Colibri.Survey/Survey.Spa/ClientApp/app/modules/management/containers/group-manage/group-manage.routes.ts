import { Routes, RouterModule } from '@angular/router';

/* component */ import { GroupManageComponent } from './group-manage.component';
/* component */ import { GroupDataComponent } from './group-data/group-data.component';
// /* component */ import { GroupDataTreeComponent } from './group-data-tree/group-data-tree.component';
/* component */ import { GroupViewComponent } from './group-view/group-view.component';
/* component */ import { GroupViewDetailComponent } from './group-view-detail/group-view-detail.component';
// /* component */ import { MemberGridComponent } from './member-grid/member-grid.component';

const routes: Routes = [
    {
        path: 'groups',
        component: GroupManageComponent,
        data: { breadcrumb: 'Group management' },
        children: [
            {
                path: '',
                component: GroupDataComponent,
                data: { breadcrumb: '' },
            },
            {
                path: ':id',
                component: GroupViewComponent,
                data: { breadcrumb: '' },
                children: [
                    {
                        path: 'detail',
                        component: GroupViewDetailComponent,
                        data: { breadcrumb: '' },
                    },
                    {
                        path: 'members',
                        // component: MemberGridComponent,
                        data: { breadcrumb: '' },
                    },
                ]
            },

        ]
    }
];

export const GroupManageRoutingModule = RouterModule.forChild(routes);
