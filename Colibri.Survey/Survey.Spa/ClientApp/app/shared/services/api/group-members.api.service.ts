// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupMembersApiService {

    apiServer: string;

    constructor(
        @Inject('SURVEY_API_URL') apiUrl: string,
        @Inject('RESTANGULAR_IDENTITYSERVER') public restangularIdentityServer: Restangular,
        // private restangular: Restangular,
    ) {
        this.apiServer = apiUrl;
    }

    getByGroup(groupId: string, searchEntry: any = null, objectFields: string[] | null = null) {
        const result = this.restangularIdentityServer
            .all('api/groups/' + groupId + '/members/search')
            .customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    addMultiple(groupId: string, emails: any[]) {
        const result = this.restangularIdentityServer
            .all('api/groups/' + groupId + '/members')
            .customPOST(emails, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }


    // getByGroup(groupId: string, objectFields: string[] | null = null) {
    //     const paramObjectFields = objectFields ? objectFields.join(',') : null;
    //     const result = this.restangular.all(`api/groups/${groupId}/members`).customGET(undefined, { fields: paramObjectFields });
    //     return result;
    // }

    deleteMemberFromGroup(groupId: string, id: string) {
        const result = this.restangularIdentityServer.one(`api/groups/${groupId}/members`, id).remove();
        return result;
    }


    setIamPolicy(data: any) {
        const result = this.restangularIdentityServer.all('api/groups/members/iamPolicy').customPOST(data, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    getIamPolicy() {
        const result = this.restangularIdentityServer.one('api/groups/members').customGET('iamPolicy');
        return result;
    }

}
