// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

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

    getFullInfo(userId: any) {
        const result = this.restangularIdentityServer.one('api/users', userId).customGET('full');
        return result;
    }

    setIamPolicy(data: any) {
        const result = this.restangularIdentityServer.all('api/users/iamPolicy').customPOST(data, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    getIamPolicy() {
        const result = this.restangularIdentityServer.one('api/users').customGET('iamPolicy');
        return result;
    }
}
