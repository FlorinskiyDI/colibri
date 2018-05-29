// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class GroupMembersApiService {

    apiServer: string;

    constructor(
        private restangular: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
        this.apiServer = apiUrl;
    }

    getByGroup(groupId: string, objectFields: string[] | null = null) {
        const paramObjectFields = objectFields ? objectFields.join(',') : null;
        const result = this.restangular.all(`api/groups/${groupId}/members`).customGET(undefined, { fields: paramObjectFields });
        return result;
    }

    deleteMemberFromGroup(groupId: string, id: string) {
        const result = this.restangular.one(`api/groups/${groupId}/members`, id).remove();
        return result;
    }

}
