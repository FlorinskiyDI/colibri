import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { SidebarModule } from 'ng-sidebar';
import { DndModule } from 'ng2-dnd';

/* module */ import { SharedModule } from 'shared/shared.module';
/* component */ import { NavbarComponent } from 'core/layout/navbar.component';
/* component */ import { LayoutComponent } from 'core/layout/layout.component';
/* component */ import { FooterLayoutComponent } from 'core/layout/footer.component';
/* component */ import { LayoutPortalComponent } from 'core/layout-portal/layout-portal.component';
/* component */ import { ForbiddenComponent } from 'core/forbidden/forbidden.component';
/* component */ import { UnauthorizedComponent } from 'core/unauthorized/unauthorized.component';
/* component */ import { LoginComponent } from 'core/login/login.component';
import { ParticlesModule } from 'angular-particle';

import { MdlMenuModule } from '@angular-mdl/core/components/menu';
import { MdlModule, MdlCheckboxModule } from '@angular-mdl/core';

declare let window: any;

@NgModule({
    declarations: [
        LayoutComponent,
        FooterLayoutComponent,
        LayoutPortalComponent,
        NavbarComponent,
        ForbiddenComponent,
        ForbiddenComponent,
        UnauthorizedComponent,
        LoginComponent,
        // MdlMenuComponent,


    ],
    exports: [
        DndModule,
        NavbarComponent,
        MdlModule,
        MdlCheckboxModule,
        MdlMenuModule

    ],
    imports: [
        DndModule.forRoot(),
        SidebarModule.forRoot(),
        SharedModule,
        MdlModule,
        MdlCheckboxModule,
        MdlMenuModule,
        ParticlesModule

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

