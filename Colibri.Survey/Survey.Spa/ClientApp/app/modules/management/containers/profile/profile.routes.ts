import { Routes, RouterModule } from '@angular/router';

/* component */ import { ProfileComponent } from './profile.component';

const routes: Routes = [
    {
        path: 'profile',
        component: ProfileComponent,
        // data: { breadcrumb: '' },
    }
];

export const ProfileRoutingModule = RouterModule.forChild(routes);
