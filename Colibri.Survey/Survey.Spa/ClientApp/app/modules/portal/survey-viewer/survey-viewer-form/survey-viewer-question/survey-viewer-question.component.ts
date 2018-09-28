
import { Component, OnInit, Input } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { QuestionControlService } from 'shared/services/question-control.service';
import { FormGroup, FormArray, FormControl, FormBuilder } from '@angular/forms';
// import { SurveysApiService } from 'shared/services/api/surveys.api.service';
// import { SurveyModel } from 'shared/models/form-builder/survey.model';
// import { PageModel } from 'shared/models/form-builder/page.model';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
import { CheckboxQuestion } from 'shared/models/form-builder/question-checkbox.model';
import { ControlOptionModel } from 'shared/models/form-builder/form-control/control-option.model';

@Component({
    selector: 'survey-viewer-question',
    templateUrl: './survey-viewer-question.component.html',
    styleUrls: [
        './survey-viewer-question.component.scss'
    ],
})
export class SurveyViewerQuestionComponent implements OnInit {



    @Input() question: QuestionBase<any>;
    @Input() pageId: string;
    @Input() form: FormGroup;

    constructor(
        private fb: FormBuilder,
        // public questionControlService: QuestionControlService,
    ) {

    }


    ngOnInit() {

    }



    onChange(questonId: string, optionId: string, isChecked: boolean, index: any) {
        console.log('work work work work work work work work work work work');

        const checkboxquestion = this.question as CheckboxQuestion;
        const option = checkboxquestion.options.find((x: ControlOptionModel) => x.id === optionId);
        option.label = isChecked;
        // const answer: any = 'answer';
        // const checkBoxArray = <FormGroup>this.form.controls[this.pageId];
        const questionArray = <FormArray>this.form.controls[this.pageId].get(questonId);
        const val: any = 'answer';
        const checkBoxControl = questionArray.controls[val] as FormArray;
        // const checkBoxArray = <FormArray>this.form.get(this.pageId).get(questonId);
        // const checkBoxControl = checkBoxArray.controls['answer'];
        console.log(this.question);
        if (isChecked) {
            checkBoxControl.push(new FormControl(optionId));
        } else {
            index = checkBoxControl.controls.findIndex((x: any) => x.value === optionId);
            checkBoxControl.removeAt(index);
        }
    }



    onChangeGridRadio(itemRowLabel: any, itemCol: any,  label: string) {
        const radioArray = <FormArray>this.form.controls[this.pageId].get(this.question.id);
        const val: any = 'answer';
        const answerControl = radioArray.controls[val] as FormArray;
        const group: any = {};

        group['row'] = this.fb.group({
            'id': new FormControl(itemRowLabel.id),
            'label': new FormControl(itemRowLabel.value)
        });
        group['col'] = this.fb.group({
            'id': new FormControl(itemCol.id),
            'label': new FormControl(itemCol.value)
        });

        const item2 = answerControl.controls.findIndex((x: any) => x.controls['row'].controls['id'].value === itemRowLabel.id);
        const needcontrol = answerControl.controls[item2];
        if (!!needcontrol) {
            answerControl.removeAt(item2);
        }
        const formGroup = new FormGroup(group);
        answerControl.push(formGroup);
    }

}
