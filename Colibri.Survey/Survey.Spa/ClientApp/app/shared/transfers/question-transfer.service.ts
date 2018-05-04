import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class QuestionTransferService {
    // private subject = new Subject<any>();
    private modelsubject = new Subject<any>();
    private questionsubject = new Subject<any>();
    private optionsubject = new Subject<any>();
    private idsubject = new Subject<any>();
    private flickersubject = new Subject<any>();
    // -start transer 1- Communicating Between Components device-details and devcie-id-mapping
    // sendDeviceID(id: string) {
    //     this.subject.next({ deviceid: id });
    // }
    // getDeviceID(): Observable<any> {
    //     return this.subject.asObservable();
    // }



    // list questio nfor select page
    setQuestions(data: any) {
        this.questionsubject.next(data);
    }
    getQuestions(): Observable<any> {
        return this.questionsubject.asObservable();
    }

    // remove question
    setDropQuestion(data: any) {
        this.modelsubject.next(data);
    }
    getDropQuestion(): Observable<any> {
        return this.modelsubject.asObservable();
    }


    // get  question
    setQuestionOption(data: any) {
        this.optionsubject.next(data);
    }
    getQuestionOption(): Observable<any> {
        return this.optionsubject.asObservable();
    }


    // for check drag control if lost focus without need area
    setDropQuestionId(id: any) {
        this.idsubject.next(id);
    }
    getDropQuestionId(): Observable<any> {
        return this.idsubject.asObservable();
    }


    // for change option field background color after click to notification button on question builder
    setFlickerOption(id: number) {
        this.flickersubject.next(id);
    }
    getFlickerOption(): Observable<any> {
        return this.flickersubject.asObservable();
    }

}
