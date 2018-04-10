import { Component, OnInit } from '@angular/core';

/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';

@Component({
    selector: 'login-component',
    templateUrl: 'login.component.html',
    styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

    constructor(
        private oidcSecurityService: OidcSecurityService
    ) {
    }

    ngOnInit() {
    }

    login() {
        console.log('Do login logic');
        this.oidcSecurityService.authorize();
    }
}
