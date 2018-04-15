import { Component, OnInit, OnDestroy, EventEmitter, Output } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';

@Component({
    selector: 'app-navigation',
    templateUrl: 'navbar.component.html',
    styleUrls: ['./navbar.component.scss']
})

export class NavbarComponent implements OnInit, OnDestroy {

    @Output()
    toggle: EventEmitter<any> = new EventEmitter<any>();

    hasAdminRole = false;
    hasDataEventRecordsAdminRole = false;

    isAuthorizedSubscription: Subscription;
    getUserDataSubscription: Subscription;
    isAuthorized: boolean;

    userDataSubscription: Subscription;
    userData: any = {};

    constructor(public oidcSecurityService: OidcSecurityService) {
    }

    ngOnInit() {
        this.isAuthorizedSubscription = this.oidcSecurityService.getIsAuthorized().subscribe(
            (isAuthorized: boolean) => {
                this.isAuthorized = isAuthorized;

                if (this.isAuthorized) {
                    console.log('isAuthorized getting data');
                }
            });

        this.userDataSubscription = this.oidcSecurityService.getUserData().subscribe(
            (userData: any) => {
                this.userData = userData;
                if (userData && userData !== '' && userData.role) {
                    for (let i = 0; i < userData.role.length; i++) {
                        if (userData.role[i] === 'dataEventRecords.admin') {
                            this.hasDataEventRecordsAdminRole = true;
                        }
                        if (userData.role[i] === 'admin') {
                            this.hasAdminRole = true;
                        }
                    }
                }

                console.log('userData getting data');
            });
    }

    ngOnDestroy(): void {
        this.isAuthorizedSubscription.unsubscribe();
        this.userDataSubscription.unsubscribe();
    }

    login() {
        console.log('Do login logic');
        this.oidcSecurityService.authorize();
    }

    logout() {
        console.log('Do logout logic');
        this.oidcSecurityService.logoff();
    }

    toggleMenu() {
        this.toggle.emit();
    }
}
