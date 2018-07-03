// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';
// import { SurveyModel } from '../../models/form-builder/survey.model';
@Injectable()
export class PortalApiService {

    apiServer: string;

    constructor(
        private restangular: Restangular,
        @Inject('SURVEY_API_URL') apiUrl: string
    ) {
        this.apiServer = apiUrl;
    }

    getAll() {
        const baseAccounts = this.restangular.all('api/portal');
        // This will query /accounts and return a observable.
        return baseAccounts.getList().map((response: any) => response.plain());
    }


    getSurvey(id: string) {
        const result = this.restangular.one('api/portal', id).get();
        return result.map((response: any) => response.plain()) ;
    }

    saveAnswers(data: any): any {
        const value = this.restangular.all('api/portal').customPOST(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return value.value;
    }
}
