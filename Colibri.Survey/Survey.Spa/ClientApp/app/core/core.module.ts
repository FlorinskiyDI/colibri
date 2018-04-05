import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

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
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule
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

