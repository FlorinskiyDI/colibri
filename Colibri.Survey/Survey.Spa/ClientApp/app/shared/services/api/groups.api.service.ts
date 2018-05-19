// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupsApiService {

    apiServer: string;

    constructor(
        private restangular: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
        this.apiServer = apiUrl;
    }

    getAll(objectFields: string[] | null = null) {
        const paramObjectFields = objectFields ? objectFields.join(',') : null;
        const result = this.restangular.all('api/groups').customGET(undefined, { fields: paramObjectFields });
        return result;
    }

    get(id: string) {
        const result = this.restangular.one('api/groups', id).get();
        return result;
    }

    getSubGroups(groupId: string) {
        const result = this.restangular.all('api/groups/' + groupId + '/subgroups');
        return result.getList();
    }

    create(data: any): any {
        const value = this.restangular.all('api/groups').post(data);
        return value;
    }

    delete(id: string) {
        const value = this.restangular.one('api/groups', id).remove();
        return value;
    }
}
