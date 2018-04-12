import { Component, OnInit, OnDestroy, ViewEncapsulation } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { OidcSecurityService } from './core/auth/services/oidc.security.service';
// import 'bootstrap';
import 'jquery';

@Component({
    selector: 'app-root',
    templateUrl: 'app.component.html',
    encapsulation: ViewEncapsulation.None,
    styleUrls: [
        '../../node_modules/bootstrap/dist/css/bootstrap.min.css',
        '../../node_modules/bootstrap/dist/css/bootstrap-theme.min.css',

        '../../node_modules/primeng/resources/primeng.css',
        '../../node_modules/primeng/resources/themes/bootstrap/theme.css',

        '../../node_modules/roboto-fontface/css/roboto/roboto-fontface.css',
        '../../node_modules/material-icons/iconfont/material-icons.css',

        '../../node_modules/@angular/material/_theming.scss',
        '../../node_modules/@angular/material/prebuilt-themes/indigo-pink.css',

        '../styles/custom.material-icons.scss',
        '../styles/custom.shared.scss',
        './app.component.scss'
    ]
})

export class AppComponent implements OnInit, OnDestroy {

    isAuthorizedSubscription: Subscription;
    isAuthorized: boolean;

    constructor(public oidcSecurityService: OidcSecurityService) {
    }

    ngOnInit() {
        this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(
            (isAuthorized: boolean) => {
                this.isAuthorized = isAuthorized;
            });

        if (window.location.hash) {
            this.oidcSecurityService.authorizedCallback();
        }
    }

    ngOnDestroy(): void {
        this.isAuthorizedSubscription.unsubscribe();
    }

    login() {
        console.log('start login');
        this.oidcSecurityService.authorize();
    }

    refreshSession() {
        console.log('start refreshSession');
        this.oidcSecurityService.authorize();
    }

    logout() {
        console.log('start logoff');
        this.oidcSecurityService.logoff();
    }
}
