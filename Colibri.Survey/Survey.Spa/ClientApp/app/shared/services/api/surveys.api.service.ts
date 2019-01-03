// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';
// import { SurveyModel } from '../../models/form-builder/survey.model';
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
        // const baseAccounts = this.restangular.all('api/surveySections');
        // // This will query /accounts and return a observable.
        // return baseAccounts.getList().map((response: any) => response.plain());
        const result = this.restangular.all('api/surveySections').customGET(undefined, null);
        return result;
    }

    // get(id: string) {
    //     const result = this.restangular.one('api/surveySections', id).get();
    //     return result;
    // }

    get(id: string) {
        const result = this.restangular.one('api/surveySections', id).get();
        return result;
    }


    save(data: any): any {
        const result = this.restangular.all('api/surveySections').customPOST(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return result;
    }
    update(data: any): any {
        const result = this.restangular.all('api/surveySections').customPUT(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return result;
    }

    changeLock(id: string, isLocked: boolean): any {
        const result = this.restangular.all(`api/surveySections/${id}/${isLocked}`).customGET(undefined, undefined, { 'Content-Type': 'application/json' });
        return result;
    }
}
