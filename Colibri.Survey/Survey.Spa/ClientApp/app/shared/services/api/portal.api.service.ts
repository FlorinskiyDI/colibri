// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
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
        return result.map((response: any) => response.plain());
    }

    saveAnswers(data: any): Observable<any> {
        const result = this.restangular.one('api/portal').customPOST(data, undefined, undefined,
            { 'Content-Type': 'application/json' });
        return result;
    }

    getAnswers(surveyId: string): Observable<any> {
        const result = this.restangular.all(`api/report/${surveyId}`).customGET(undefined, undefined);

        // const result = this.restangular.all(`api/report`).customGET(undefined, {surveyId: surveyId});
        return result;

        // const result = this.restangular.all(`api/portal`).customGET(undefined, { id: id });
        // console.log();
        // return result;
        // const result = this.restangular.one('api/portal', id).get();
        // return result.map((response: any) => response.plain()) ;
    }
}
