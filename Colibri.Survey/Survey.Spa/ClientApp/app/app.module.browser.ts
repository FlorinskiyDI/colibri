import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './app.component';
import { NavigationComponent } from './navigation/navigation.component';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserManagementComponent } from './user-management/user-management.component';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

declare let window: any;

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavigationComponent,
        ForbiddenComponent,
        ForbiddenComponent,
        HomeComponent,
        UnauthorizedComponent,
        UserManagementComponent
    ],
    imports: [
        RouterModule,
        HttpClientModule,
        AppModuleShared,
        BrowserAnimationsModule
    ],
    providers: [
        { provide: 'API_URL', useValue: getApiUrl() }
    ]
})
export class AppModule {
}

export function getApiUrl() {
    return window['serverSettings']!.AlarmApiUrl;
}

