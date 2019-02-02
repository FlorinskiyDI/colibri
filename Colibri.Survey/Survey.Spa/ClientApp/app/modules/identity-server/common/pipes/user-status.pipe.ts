import { Pipe, PipeTransform } from '@angular/core';

/* service */ import { UserService } from '../services/user.service';

export enum Statues {
    registered = 'Registered',
    invite = 'Invited',
    inviteExpired = 'Invitation expired',
}

@Pipe({ name: 'userstatus' })
export class UserStatusPipe implements PipeTransform {

    constructor(
        private userService: UserService
    ) { }

    transform(value: any, ...args: any[]): any {
        if (value) {
            return Statues.registered;
        } else {
            const invitationDate = args[0];
            const lifespan = args[1];
            // const isExpired = this.checkIsExpired(invitationDate, lifespan);
            const isExpired = this.userService.checkIsExpired(invitationDate, lifespan);
            return isExpired ? Statues.inviteExpired : Statues.invite;
        }
    }

}
