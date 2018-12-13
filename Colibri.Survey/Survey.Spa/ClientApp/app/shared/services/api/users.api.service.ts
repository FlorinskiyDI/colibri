// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';
import { RESTANGULAR_IDENTITYSERVER } from '../../../app.module.browser';

@Injectable()
export class UsersApiService {

    constructor(
        // private restangular: Restangular,
        @Inject('RESTANGULAR_IDENTITYSERVER') public restangularIdentityServer: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
    }

    getAll(searchEntry: any = null, objectFields: string[] | null = null) {
        const result = this.restangularIdentityServer.all('api/users/search').customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    sendInvite(userId: any) {
        const result = this.restangularIdentityServer.one('api/users', userId).customGET('invite');
        return result;
    }
}
