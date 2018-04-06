import { NgModule } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* component */ import { NavbarComponent } from 'core/layout/navbar.component';
/* component */ import { ForbiddenComponent } from 'core/forbidden/forbidden.component';
/* component */ import { UnauthorizedComponent } from 'core/unauthorized/unauthorized.component';
declare let window: any;

@NgModule({
    declarations: [
        NavbarComponent,
        ForbiddenComponent,
        ForbiddenComponent,
        UnauthorizedComponent
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

