// external import
import { Injectable, Inject } from '@angular/core';
import { Restangular } from 'ngx-restangular';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
// import {  map, share } from 'rxjs/operators';
// import { HttpClient } from '@angular/common/http';
import { Http, RequestOptions, Headers, ResponseContentType } from '@angular/http';

// import { SurveyModel } from '../../models/form-builder/survey.model';
@Injectable()
export class PortalApiService {

    apiServer: string;

    constructor(
        private http: Http,
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
        return result;
    }


    // getExcelReport(quizId: string): Observable<any> {
    //     const result = this.restangular.all(`api/report/${quizId}`).customGET(undefined, undefined);
    //     return result;
    // }


    // getExcelReport(quizId: string) {
    //     console.log('111111111111112222222222222222222222222223333333333333333');
    //     debugger
    //     const url = this.apiServer + `api/report/${quizId}`;
    //     const options = this.getOptionRequestFile(false);

    //     return this.http.post(url, {}, options);
    // }


    getExcelReport(quizId: string) {
        const url = this.apiServer + `api/report/DownloadGrid/${quizId}`;
        const body = {};
        const options = this.getOptionRequestFile();
        options.responseType = ResponseContentType.Blob;
        return this.http.post(url, body, options);
    }




    getOptionRequestFile(authorized?: boolean): any {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        authorized = authorized || true;
        // if (authorized) {
        //     headers.set('Authorization', 'this.getAuthToken()');
        // }
        return new RequestOptions({ headers: headers, responseType: 2 as 1 }); // 2 - arrayBuffer, 1 - json
    }



    getOptionRequest(authorized?: boolean): RequestOptions {
        const headers = new Headers({ 'Content-Type': 'application/json' });
        authorized = authorized || true;

        if (authorized) {
            headers.set('Authorization', this.getAuthToken());
        }

        return new RequestOptions({ headers: headers });
    }



    getAuthToken() {
        return '';
        // const userJson = localStorage.getItem('currentUser');
        // let token = '';

        // if (userJson) {
        //     const user = JSON.parse(userJson);
        //     token = user.token;
        // }

        // return `Bearer ${token}`;
    }

}
