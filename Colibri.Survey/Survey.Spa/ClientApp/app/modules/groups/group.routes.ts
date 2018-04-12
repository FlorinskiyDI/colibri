import { Routes, RouterModule } from '@angular/router';

/* component */ import { GroupComponent } from './group.component';
/* component */ import { GroupGridComponent } from './group-grid/group-grid.component';

const routes: Routes = [
    {
        path: 'groups',
        component: GroupComponent,
        data: { breadcrumb: 'Group managment' },
        children: [
            {
                path: '',
                component: GroupGridComponent,
                data: { breadcrumb: 'Grid' },
            },
        ]
    }
];

export const GroupRoutingModule = RouterModule.forChild(routes);
