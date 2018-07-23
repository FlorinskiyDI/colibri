
import { Component, OnInit, OnChanges, Input, EventEmitter, Output, SimpleChange } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
import { QuestionControlService } from 'shared/services/question-control.service';
import { FormGroup } from '@angular/forms';
// import { SurveysApiService } from 'shared/services/api/surveys.api.service';
// import { SurveyModel } from 'shared/models/form-builder/survey.model';
import { PageModel } from 'shared/models/form-builder/page.model';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
// import { QuestionService } from '../services/builder.service';

@Component({
    selector: 'survey-viewer-form',
    templateUrl: './survey-viewer-form.component.html',
    styleUrls: [
        './survey-viewer-form.component.scss'
    ],
    providers: [QuestionControlService],
    // template: `<router-outlet></router-outlet>`
})
export class SurveyViewerFormComponent implements OnInit, OnChanges {

    @Input() unfilledQestionId: string;
    @Input() paging: any;
    @Input() page: PageModel = new PageModel();
    @Output() filledAnswers = new EventEmitter<any>();

    form: FormGroup;
    pageIndex: number;
    constructor(
        private questionTransferService: QuestionTransferService,
        public questionControlService: QuestionControlService,
    ) {
        window.scroll(0, 0);
        this.questionTransferService.getFormViewrPage().subscribe((page: any) => { // updata formbuild after select page
            if (page) {

                const val = this.questionControlService.toFormGroup(page.questions);
                this.form.addControl(page.id, val);
                this.pageIndex = this.paging.find((item: any) => item.id === page.id).index;
            }
        });

    }


    ngOnInit() {


        this.pageIndex = this.paging.find((item: any) => item.id === this.page.id).index;

        const page: any = {};
        page[this.page.id] = this.questionControlService.toFormGroup(this.page.questions);
        this.form = new FormGroup(page);
    }

    goToNextPage() {

        const idNextPage = this.paging[this.pageIndex + 1].id;
        this.questionTransferService.setViewerPage(idNextPage);
    }


    goToPreviewPage() {

        const idNextPage = this.paging[this.pageIndex - 1].id;
        this.questionTransferService.setViewerPage(idNextPage);
    }


    sendAnswer() {
        this.filledAnswers.emit(this.form);
    }



    ngOnChanges(changes: { [propertyName: string]: SimpleChange }) {


        setTimeout(() => {
            this.unfilledQestionId = null;
        }, 1000); // time as transition property in css

    }

}
