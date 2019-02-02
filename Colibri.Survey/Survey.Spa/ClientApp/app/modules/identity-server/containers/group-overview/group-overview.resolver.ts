import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';

@Injectable()
export class GroupOverviewResolve implements Resolve<any> {

    constructor(public oidcSecurityService: OidcSecurityService) { }

    resolve(route: ActivatedRouteSnapshot) {
        const id = route.paramMap.get('id');
        this.oidcSecurityService.setCustomRequestParameters({ acr_values: 'tenant:' + id });
        this.oidcSecurityService.refreshSession();
        return id;
    }
}
