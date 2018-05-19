import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { RestangularModule } from 'ngx-restangular';

/* module */ import { CoreModule } from 'core/core.module';
/* module */ import { AppModuleShared } from './app.module.shared';
/* component */ import { AppComponent } from './app.component';
/* component */ import { HomeComponent } from './home/home.component';
/* component */ import { UserManagementComponent } from './user-management/user-management.component';
/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';
/* helpers */ import { Helpers } from 'shared/helpers/helpers';
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
        RestangularModule.forRoot([OidcSecurityService], RestangularConfigFactory),
        RouterModule,
        HttpClientModule,
        CoreModule,
        AppModuleShared,
        BrowserAnimationsModule
    ],
    providers: [
        { provide: 'SURVEY_API_URL', useValue: getApiUrl() },
        { provide: 'IDENTITY_SERVER_API_URL', useValue: getApiUrl() }
    ]
})

export class AppModule {
}

export function getApiUrl() {
    const url = window['serverSettings'].ServeyApiUrl;
    return Helpers.endsWithSlash(url);
}

// Function for setting the default restangular configuration
export function RestangularConfigFactory(RestangularProvider: any, oidcSecurityService: OidcSecurityService) {
    const serveyApiUrl: any = Helpers.endsWithSlash(window['serverSettings'].ServeyApiUrl);
    if (!serveyApiUrl) { console.error('!!! There are no server settings'); }

    RestangularProvider.setBaseUrl(serveyApiUrl);
    // by each request to the server receive a token and update headers with it
    RestangularProvider
        .addFullRequestInterceptor((element: any, operation: any, path: any, url: any, headers: any, params: any) => {
            const bearerToken = oidcSecurityService.getToken();
            return {
                headers: Object.assign({},
                    headers,
                    { Authorization: `Bearer ${bearerToken}` }
                )
            };
        })
        .addResponseInterceptor((data: any, operation: any, what: any, url: any, response: any) => {
            switch (operation) {
                case 'post':
                case 'put':
                case 'remove':
                    if (!data) {
                        return {};
                    }
                    break;
                default:
                    return data;
            }
        });
}
