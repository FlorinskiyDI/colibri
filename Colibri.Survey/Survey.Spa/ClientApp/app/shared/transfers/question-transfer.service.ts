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

    private deletePagesubject = new Subject<any>();
    private pageidsubject = new Subject<any>();

    private changeQuestionsubject = new Subject<any>();
    private chagnedQuestionSubject = new Subject<any>();



    //
    private selectedPageSubject = new Subject<any>();
    private PageSubject = new Subject<any>();
    private ExternalQuestionSubect = new Subject<any>();
    private pageIdSubect = new Subject<any>();
    private deleteQuestion = new Subject<any>();
    // private statePageSubject = new Subject<any>();

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


    // for change option field background color after click to notification button on question builder
    setdeletePageId(id: any) {
        this.deletePagesubject.next(id);
    }
    getdeletePageId(): Observable<any> {
        return this.deletePagesubject.asObservable();
    }


    // for change option field background color after click to notification button on question builder
    setIdByNewPage(id: any) {
        this.pageidsubject.next(id);
    }
    getIdByNewPage(): Observable<any> {
        return this.pageidsubject.asObservable();
    }


    // for change option field background color after click to notification button on question builder
    setDataForChangeQuestion(data: any) {
        this.changeQuestionsubject.next(data);
    }
    getDataForChangeQuestion(): Observable<any> {
        return this.changeQuestionsubject.asObservable();
    }


    // for change option field background color after click to notification button on question builder
    //  setStatePageAsChange(data: any) {
    //     this.statePageSubject.next(data);
    // }
    // getPageState(): Observable<any> {
    //     return this.statePageSubject.asObservable();
    // }

    setChangedQuestion(data: any) {
        this.chagnedQuestionSubject.next(data);
    }
    getChangedQuestion(): Observable<any> {
        return this.chagnedQuestionSubject.asObservable();
    }



    setSelectedPage(data: any) {
        this.selectedPageSubject.next(data);
    }
    getSelectedPage(): Observable<any> {
        return this.selectedPageSubject.asObservable();
    }


    setFormPage(data: any) {
        this.PageSubject.next(data);
    }
    getFormPage(): Observable<any> {
        return this.PageSubject.asObservable();
    }


    setQuestionForDelete(data: any) {
        this.ExternalQuestionSubect.next(data);
    }
    getQuestionForDelete(): Observable<any> {
        return this.ExternalQuestionSubect.asObservable();
    }




    setPageById(data: any) {
        this.pageIdSubect.next(data);
    }
    getPageById(): Observable<any> {
        return this.pageIdSubect.asObservable();
    }

    setDeleteDragQuestion(id: any) {
        this.deleteQuestion.next(id);
    }
    getDeleteDragQuestion(): Observable<any> {
        return this.deleteQuestion.asObservable();
    }
}
