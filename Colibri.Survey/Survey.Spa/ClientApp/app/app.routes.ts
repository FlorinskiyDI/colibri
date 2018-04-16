import { Routes, RouterModule } from '@angular/router';

/* component */ import { ForbiddenComponent } from './core/forbidden/forbidden.component';
/* component */ import { HomeComponent } from './home/home.component';
/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { UnauthorizedComponent } from './core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from './core/login/login.component';
/* component */ import { UserManagementComponent } from './user-management/user-management.component';

/* guard */ import { HasAdminRoleAuthenticationGuard } from './guards/hasAdminRoleAuthenticationGuard';
/* guard */ import { HasAdminRoleCanLoadGuard } from './guards/hasAdminRoleCanLoadGuard';

const appRoutes: Routes = [
    {
        path: '', component: LayoutComponent,
        // canActivate: [HasAdminRoleAuthenticationGuard],
        data: { breadcrumb: 'Layout' },
        children: [
            {
                path: 'home',
                component: HomeComponent,
                data: { breadcrumb: 'Home page' },
            },
            {
                path: '',
                loadChildren: 'modules/groups/group.module#GroupModule',
            },
            {
                path: '',
                loadChildren: 'modules/users/user.module#UserModule',
            },
            {
                path: '',
                loadChildren: 'modules/surveys/survey.module#SurveyModule',
            },
        ]
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
