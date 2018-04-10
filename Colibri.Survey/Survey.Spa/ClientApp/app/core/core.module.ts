import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* component */ import { NavbarComponent } from 'core/layout/navbar.component';
/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { ForbiddenComponent } from 'core/forbidden/forbidden.component';
/* component */ import { UnauthorizedComponent } from 'core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from 'core/login/login.component';
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
        SharedModule
    ],
    providers: [
        { provide: 'API_URL', useValue: getApiUrl() }
    ]
})

export class CoreModule {
}

export function getApiUrl() {
    return window['serverSettings']!.AlarmApiUrl;
}

