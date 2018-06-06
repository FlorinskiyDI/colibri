import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { SidebarModule } from 'ng-sidebar';
import { DndModule } from 'ng2-dnd';

/* module */ import { SharedModule } from 'shared/shared.module';
/* component */ import { NavbarComponent } from 'core/layout/navbar.component';
/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { LayoutPortalComponent } from 'core/layout-portal/layout-portal.component';
/* component */ import { ForbiddenComponent } from 'core/forbidden/forbidden.component';
/* component */ import { UnauthorizedComponent } from 'core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from 'core/login/login.component';
declare let window: any;

@NgModule({
    declarations: [
        LayoutComponent,
        LayoutPortalComponent,
        NavbarComponent,
        ForbiddenComponent,
        ForbiddenComponent,
        UnauthorizedComponent,
        LoginComponent
    ],
    exports: [
        DndModule,
        NavbarComponent,

    ],
    imports: [
        DndModule.forRoot(),
        SidebarModule.forRoot(),
        SharedModule
    ],
    providers: [
        { provide: 'API_URL', useValue: getApiUrl() }
    ],
    schemas: [NO_ERRORS_SCHEMA]
})

export class CoreModule {
}

export function getApiUrl() {
    return window['serverSettings']!.AlarmApiUrl;
}

