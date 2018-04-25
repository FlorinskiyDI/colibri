import { Component, ViewEncapsulation } from '@angular/core';
import { QuestionTransferService } from '../../../../shared/transfers/question-transfer.service';

import { FormGroup, Validators } from '@angular/forms';
@Component({
    selector: 'question-option',
    templateUrl: './question-option.component.html',
    styleUrls: [
        './question-option.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})
export class QuestionOptionComponent {

    questionOption: any = {};
    questionControl: FormGroup;
    constructor(
        private questionTransferService: QuestionTransferService,
        // private fb: FormBuilder
    ) {
        this.questionTransferService.getQuestionOption().subscribe((data: any) => {
            this.questionOption = data.question;
            this.questionControl = data.control;
            // const answerControl = data.control.get['answer'];
            // answerControl.clearValidators();
            // answerControl.updateValueAndValidity();

            console.log('111111111111111111111');

        });
    }
    changeQuestionValidation(state: boolean) {
        if (state) {
            console.log(this.questionControl);
            this.questionControl.controls['answer'].setValidators(Validators.required);
            this.questionControl.controls['answer'].updateValueAndValidity();

        } else {


            this.questionControl.controls['answer'].clearValidators();
            this.questionControl.controls['answer'].updateValueAndValidity();
        }
    }


    AddAdditionalQuestion (state: boolean) {
        if (!state) {
            this.questionControl.controls['additionalAnswer'].setValue('');
        }
    }
}
