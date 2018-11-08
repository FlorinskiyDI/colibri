import { Injectable } from '@angular/core';
import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
    HttpResponse
} from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { Router } from '@angular/router';


@Injectable()
export class AuthHttpInterceptor implements HttpInterceptor {

    constructor(
        private router: Router
    ) { }


    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // Clone the request to add the new header.
        const authReq: HttpRequest<any> = req.clone();
        // send the newly created request
        return next.handle(authReq)
            .do((event: any) => { return event; },
                (error: any) => {
                    if (error instanceof HttpErrorResponse) {
                        if (error.status === 401) {
                            // intercept the respons error and displace it to the console
                            this.router.navigate(['/login']);
                            // return the error to the method that called it
                            return Observable.throw(error);
                        }
                    }
                    return Observable.throw(error);
                });
    }
}
