import { Pipe, PipeTransform } from '@angular/core';
import * as moment from 'moment';

export enum Statues {
    registered = 'Registered',
    invite = 'Invited',
    inviteExpired = 'Invitation expired',
}

@Pipe({ name: 'userstatus' })
export class UserStatusPipe implements PipeTransform {

    transform(value: any, ...args: any[]): any {
        if (value) {
            return Statues.registered;
        } else {
            const invitationDate = args[0];
            const lifespan = args[1];
            const isExpired = this.checkIsExpired(invitationDate, lifespan);
            return isExpired ? Statues.inviteExpired : Statues.invite;
        }
    }

    private checkIsExpired(invitationDate: any, lifespan: any) {
        if (invitationDate == null || lifespan == null) {
            console.error('An error occurred while checking invitation date expired');
        }
        // const momentInvitationDate = moment(invitationDate);
        // const nowInMiliseconds = moment().valueOf();
        // const momentInvitationDateExpired = moment(nowInMiliseconds + lifespan);
        // const isExpired = momentInvitationDateExpired.isBefore(momentInvitationDate);

        // const momentInvitationDate = moment(invitationDate);
        const nowInMiliseconds = moment(invitationDate).valueOf();
        const momentInvitationDateExpired = moment(nowInMiliseconds + lifespan);
        const isExpired = moment().isAfter(momentInvitationDateExpired);

        return isExpired;
    }
}

