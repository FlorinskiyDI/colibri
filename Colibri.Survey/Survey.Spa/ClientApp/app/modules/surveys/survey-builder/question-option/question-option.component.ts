import { Component, ViewEncapsulation } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';

// import { GridRadioQuestion } from '../../../../shared/models/form-builder/question-base.model';
import { QuestionTransferService } from '../../../../shared/transfers/question-transfer.service';
import { ControTypes } from '../../../../shared/constants/control-types.constant';
// import { FormControl } from '@angular/forms/src/model';
@Component({
    selector: 'question-option',
    templateUrl: './question-option.component.html',
    styleUrls: [
        './question-option.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})
export class QuestionOptionComponent {

    divRequied = false;
    divAdditional = false;

    questionOption: any = null;
    questionControl: FormGroup;
    constructor(
        private questionTransferService: QuestionTransferService,
        // private fb: FormBuilder
    ) {
        this.questionTransferService.getQuestionOption().subscribe((data: any) => {
            if (data != null) {
                this.questionOption = data.question;
                this.questionControl = data.control;
            } else {
                this.questionOption = null;
                this.questionControl = null;
            }
        });
        this.questionTransferService.getFlickerOption().subscribe((id: number) => {
            if (id === 1) {
                this.divRequied = true;
                setTimeout(() => {
                    this.divRequied = false;
                }, 500); // time as transition property in css
            }
            if (id === 2) {
                this.divAdditional = true;
                setTimeout(() => {
                    this.divAdditional = false;
                }, 500); // time as transition property in css
            }

        });
    }
    changeQuestionValidation(state: boolean) {
        console.log(this.questionOption);
        switch (this.questionOption.controlType) {

            case ControTypes.gridRadio: {

                const gridQuestion = this.questionOption;
                const rowsGroup: any = this.questionControl.controls['rows'];
                gridQuestion.grid.rows.forEach((item: any) => {
                    if (state) {
                        console.log('set require grid');
                        rowsGroup.controls[item.id].controls['label'].setValidators(Validators.required);
                        rowsGroup.controls[item.id].controls['label'].updateValueAndValidity();
                    } else {
                        console.log('delete require grid');
                        rowsGroup.controls[item.id].controls['label'].clearValidators();
                        rowsGroup.controls[item.id].controls['label'].updateValueAndValidity();
                    }
                });
                break;
            }
            default: {
                if (state) {

                    this.questionControl.controls['answer'].setValidators(Validators.required);
                    this.questionControl.controls['answer'].updateValueAndValidity();
                } else {
                    this.questionControl.controls['answer'].clearValidators();
                    this.questionControl.controls['answer'].updateValueAndValidity();
                }
            }
        }


    }


    AddAdditionalQuestion(state: boolean) {
        if (!state) {
            this.questionControl.controls['additionalAnswer'].setValue('');
        }
    }
}
