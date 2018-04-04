import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { RestangularModule, Restangular } from 'ngx-restangular';

/* module */ import { CoreModule } from 'core/core.module';
/* module */ import { AppModuleShared } from './app.module.shared';
/* component */ import { AppComponent } from './app.component';
/* component */ import { HomeComponent } from './home/home.component';
/* component */ import { UserManagementComponent } from './user-management/user-management.component';
/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';
declare let window: any;



@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        HomeComponent,
        UserManagementComponent
    ],
    imports: [
        // Importing RestangularModule and making default configs for restanglar
        RestangularModule.forRoot(RestangularConfigFactory),
        RouterModule,
        HttpClientModule,
        CoreModule,
        AppModuleShared,
        BrowserAnimationsModule
    ],
    providers: [ ]
})

export class AppModule {
}

// Function for setting the default restangular configuration
export function RestangularConfigFactory (RestangularProvider: any, oidcSecurityService: OidcSecurityService) {
    RestangularProvider.setBaseUrl(window['serverSettings']!.AlarmApiUrl);
    // by each request to the server receive a token and update headers with it
    RestangularProvider.addFullRequestInterceptor((element: any, operation: any, path: any, url: any, headers: any, params: any) => {
        debugger
        let bearerToken = oidcSecurityService.getToken();        
        return { headers: Object.assign({}, headers, {Authorization: `Bearer ${bearerToken}`}) };
    });   
}