// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupsApiService {

    apiServer: string;

    constructor(
        @Inject('RESTANGULAR_IDENTITYSERVER') public restangularIdentityServer: Restangular,
        private restangular: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
        this.apiServer = apiUrl;
    }

    getAll(searchEntry: any = null, objectFields: string[] | null = null) {
        const result = this.restangularIdentityServer.all('api/groups/search').customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    getAllRoot(searchEntry: any = null, objectFields: string[] | null = null) {
        const result = this.restangularIdentityServer.all('api/groups/root').customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    getSubgroups(searchEntry: any = null, groupId: string) {
        const result = this.restangularIdentityServer.all('api/groups/' + groupId + '/subgroups').customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }

    delete(id: string) {
        const value = this.restangularIdentityServer.one('api/groups', id).remove();
        return value;
    }

    getItem(id: string) {
        const result = this.restangularIdentityServer.one('api/groups', id).get();
        return result;
    }

    get(id: string) {
        const result = this.restangularIdentityServer.one('api/groups', id).get();
        return result;
    }

    getRoot(searchEntry: any = null, objectFields: string[] | null = null) {
        // const result = this.restangularIdentityServer.all('api/groups/root').post(searchEntry);
        const result = this.restangularIdentityServer.all('api/groups/root').customPOST(searchEntry, undefined, undefined, { 'Content-Type': 'application/json' });
        // const result = this.restangularIdentityServer.one('api/groups/root').customPost(undefined, objectFields ? objectFields.join(',') : undefined);
        return result;
    }

    create(data: any): any {
        const value = this.restangularIdentityServer.all('api/groups').post(data);
        return value;
    }

    update(data: any): any {
        const value = this.restangularIdentityServer.all('api/groups').customPUT(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return value;
    }

    /* group members */

    addMembers(groupId: string, data: string[]): any {
        const value = this.restangularIdentityServer.all('api/groups/' + groupId + '/members').post(data);
        return value;
    }
}
