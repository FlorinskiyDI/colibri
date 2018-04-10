import { Routes, RouterModule } from '@angular/router';

import { ForbiddenComponent } from './core/forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './core/unauthorized/unauthorized.component';
import { LoginComponent } from './core/login/login.component';

import { UserManagementComponent } from './user-management/user-management.component';

import { HasAdminRoleAuthenticationGuard } from './guards/hasAdminRoleAuthenticationGuard';
import { HasAdminRoleCanLoadGuard } from './guards/hasAdminRoleCanLoadGuard';

const appRoutes: Routes = [
    { 
        path: '', component: HomeComponent,
        canActivate: [HasAdminRoleAuthenticationGuard],
    },    
    { path: 'login', component: LoginComponent },
    { path: 'uihome', component: HomeComponent },
    {
        path: 'usermanagement', component: UserManagementComponent,
        canActivate: [HasAdminRoleAuthenticationGuard],
        canLoad: [HasAdminRoleCanLoadGuard]
    },
    { path: 'Forbidden', component: ForbiddenComponent },
    { path: 'Unauthorized', component: UnauthorizedComponent }
];

export const routing = RouterModule.forRoot(appRoutes);
