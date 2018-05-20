// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';

@Injectable()
export class SurveysApiService {

    apiServer: string;

    constructor(
        private restangular: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
        this.apiServer = apiUrl;
    }

    getAll() {
        const baseAccounts = this.restangular.all('api/surveys');
        // This will query /accounts and return a observable.
        return baseAccounts.getList();
        // const value = this.restangular.one(this.apiServer).getList('api/groups');
        // return value;
    }

    get(id: string) {
        const result = this.restangular.one('api/surveySections', id).get();
        return result;
    }

    saveSurvey(data: any): any {
        const value = this.restangular.all('api/surveySections').customPOST(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return value;
    }
}
