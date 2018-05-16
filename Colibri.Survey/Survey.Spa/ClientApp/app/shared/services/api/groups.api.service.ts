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

    getAll() {
        const result = this.restangular.all('api/groups');
        return result.getList();
    }

    getSubGroups(groupId: string) {
        const result = this.restangular.all('api/groups/' + groupId + '/subgroups');
        return result.getList();
    }
}
