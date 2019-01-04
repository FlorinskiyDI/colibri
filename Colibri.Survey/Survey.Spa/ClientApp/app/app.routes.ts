import { Routes, RouterModule } from '@angular/router';

/* component */ import { ForbiddenComponent } from './core/forbidden/forbidden.component';
/* component */ import { HomeComponent } from './home/home.component';

/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { LayoutPortal2Component } from 'core/layout-portal/layout-portal.component';
/* component */ import { LayoutIdentityComponent } from 'core/layout/layout-identity/layout-identity.component';
/* component */ import { LayoutPortalComponent } from 'core/layout/layout-portal/layout-portal.component';
/* component */ import { LayoutDashboardComponent } from 'core/layout/layout-dashboard/layout-dashboard.component';

/* component */ import { UnauthorizedComponent } from './core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from './core/login/login.component';

/* guard */ import { HasAdminRoleAuthenticationGuard } from './guards/hasAdminRoleAuthenticationGuard';
// /* guard */ import { HasAdminRoleCanLoadGuard } from './guards/hasAdminRoleCanLoadGuard';

const appRoutes: Routes = [
    {
        path: 'is', component: LayoutIdentityComponent,
        canActivate: [HasAdminRoleAuthenticationGuard],
        children: [            
            {
                path: '',
                loadChildren: 'modules/identity-server/management.module#ManagementModule',
            },
            // {
            //     path: '',
            //     loadChildren: 'modules/management/management.module#ManagementModule',
            // },
            // {
            //     path: '',
            //     loadChildren: 'modules/dashboard/dashboard.module#DashboardModule',
            // },
            // {
            //     path: '',
            //     loadChildren: 'modules/surveys/survey.module#SurveyModule',
            // },
        ]
    },
    {
        path: 'portal2', component: LayoutPortalComponent,
        canActivate: [HasAdminRoleAuthenticationGuard],

    },
    {
        path: 'dashboard', component: LayoutDashboardComponent,
        canActivate: [HasAdminRoleAuthenticationGuard],

    },
    {
        path: '',
        component: LayoutPortal2Component,
        //  canActivate: [HasAdminRoleAuthenticationGuard],
        // data: { breadcrumb: 'Layout' },
        children: [
            {
                path: '',
                loadChildren: 'modules/portal/portal.module#PortalModule',
            },
            // {
            //     path: 'home',
            //     component: HomeComponent,
            //     data: { breadcrumb: 'Home page' },
            // }
        ]
    },



    { path: 'login', component: LoginComponent },
    { path: 'uihome', component: HomeComponent },
    { path: 'Forbidden', component: ForbiddenComponent },
    { path: 'Unauthorized', component: UnauthorizedComponent }
];

export const routing = RouterModule.forRoot(appRoutes);
