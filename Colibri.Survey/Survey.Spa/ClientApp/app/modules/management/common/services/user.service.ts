// external import
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';
import * as moment from 'moment';

@Injectable()
export class UserService {

    constructor() { }

    public checkIsExpired(invitationDate: any, lifespan: any) {
        if (invitationDate == null || lifespan == null) {
            console.error('An error occurred while checking invitation date expired');
        }
        //
        const nowInMiliseconds = moment(invitationDate).valueOf();
        const momentInvitationDateExpired = moment(nowInMiliseconds + lifespan);
        const isExpired = moment().isAfter(momentInvitationDateExpired);
        //
        return isExpired;
    }
}
