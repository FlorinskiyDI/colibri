// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupRolesApiService {

    apiServer: string;

    constructor(
        @Inject('SURVEY_API_URL') apiUrl: string,
        @Inject('RESTANGULAR_IDENTITYSERVER') public restangularIdentityServer: Restangular,
        // private restangular: Restangular,
    ) {
        this.apiServer = apiUrl;
    }

    get() {
        const result = this.restangularIdentityServer.one('api/groups/roles').customGET('');
        return result;
    }

}
