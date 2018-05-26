import {
    Component, Input, AfterContentChecked, ChangeDetectorRef, AfterViewInit,
    OnDestroy, ChangeDetectionStrategy
} from '@angular/core';

import { FormGroup } from '@angular/forms';
import { Subscription } from 'rxjs/Subscription';
// import { QuestionTransferService } from 'shared/transfers/question-transfer.service';

import { AnswerControlService } from 'shared/Services/answer-control.service';


@Component({
    selector: 'question-form-builder',
    templateUrl: './question-form-builder.component.html',
    styleUrls: ['./question-form-builder.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [AnswerControlService],
})
export class QuestionFormBuilderComponent implements AfterContentChecked, OnDestroy, AfterViewInit {

    @Input() isEditState: boolean;
    @Input() question: any;
    @Input() formPage: FormGroup;
    @Input() pageId: any;
    private sub = new Subscription();

    constructor(
        private cdr: ChangeDetectorRef,
    ) {

    }


    ngOnInit() {
        console.log('22222222222222222222');
        console.log(this.formPage);
        console.log('22222222222222222222');
        console.log('22222222222222222222');
        debugger
    }



    ngOnDestroy() {
        this.sub.unsubscribe();
    }


    ngAfterContentChecked() {
        this.cdr.detectChanges();
    }


    ngAfterViewInit() {

    }
}
