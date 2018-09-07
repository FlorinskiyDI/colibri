import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable()
export class GroupManageTransferService {

    private selectedGroupIdSubject = new Subject<any>();
    private updatedGroupSubject = new Subject<any>();

    // transfer - selected group id
    sendSelectedGroupId(data: string) { this.selectedGroupIdSubject.next(data); }
    getSelectedGroupId(): Observable<string> { return this.selectedGroupIdSubject.asObservable(); }

    // transfer - updated group
    sendUpdatedGroup(data: any) { this.updatedGroupSubject.next(data); }
    getUpdatedGroup(): Observable<any> { return this.updatedGroupSubject.asObservable(); }
}
