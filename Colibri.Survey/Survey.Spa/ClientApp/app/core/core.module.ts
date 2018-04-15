import { NgModule,  NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* component */ import { NavbarComponent } from 'core/layout/navbar.component';
/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { ForbiddenComponent } from 'core/forbidden/forbidden.component';
/* component */ import { UnauthorizedComponent } from 'core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from 'core/login/login.component';
import { SidebarModule } from 'ng-sidebar';
declare let window: any;

@NgModule({
    declarations: [
        LayoutComponent,
        NavbarComponent,
        ForbiddenComponent,
        ForbiddenComponent,
        UnauthorizedComponent,
        LoginComponent
    ],
    exports: [
        NavbarComponent
    ],
    imports: [
        SidebarModule.forRoot(),
        SharedModule
    ],
    providers: [
        { provide: 'API_URL', useValue: getApiUrl() }
    ],
    schemas: [NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA]
})

export class CoreModule {
}

export function getApiUrl() {
    return window['serverSettings']!.AlarmApiUrl;
}

