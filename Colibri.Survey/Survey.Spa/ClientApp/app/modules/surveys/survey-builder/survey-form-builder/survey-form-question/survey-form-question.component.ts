import {
    Component, Input, AfterContentChecked,
    ViewChildren, QueryList, ChangeDetectorRef, AfterViewInit, OnDestroy,
    ChangeDetectionStrategy
} from '@angular/core';
import { FormControl, FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { Subscription } from 'rxjs/Subscription';

import { QuestionBase } from '../../../../../shared/models/form-builder/question-base.model';
import { CheckboxQuestion } from '../../../../../shared/models/form-builder/question-checkbox.model';
import { ControlOptionModel } from '../../../../../shared/models/form-builder/form-control/control-option.model';
import { AnswerControlService } from '../../../../../shared/Services/answer-control.service';
import { GUID } from '../../../../../shared/helpers/guide-type.helper';
@Component({
    selector: 'survey-form-question',
    templateUrl: './survey-form-question.component.html',
    styleUrls: ['./survey-form-question.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
    providers: [AnswerControlService],
})
export class SurveyFormQuestionComponent implements AfterContentChecked, OnDestroy, AfterViewInit {

    isChangeRow = true;
    lengthRows = 0;
    lengthItems = 0;
    lastSelectRowId: any;
    @ViewChildren('inputRow') rows: QueryList<any>;
    @ViewChildren('inputCol') cols: QueryList<any>;
    @Input() question: QuestionBase<any>;
    @Input() pageId: string;
    @Input() form: FormGroup;
    @Input() isEditQuestion: boolean;
    private sub = new Subscription();

    get isValid() {
        return this.form.controls[this.question.id].valid;
    }
    // get isDirty() { return this.form.controls[this.question.id].dirty; }


    constructor(
        private answerControlService: AnswerControlService,
        private cdr: ChangeDetectorRef,
        private fb: FormBuilder) { }


    ngOnInit() {
        console.log('111111111111111111111111111111111111111111111111111111111111111111111111111111111111_survey-form-question');
        if (this.question.grid) {

            this.lengthRows = this.question.grid.rows.length;
            this.lengthItems = this.question.grid.cols.length;
            console.log(this.lengthRows);
            console.log(this.lengthItems);
        } else {
            if (this.question.options) {
                this.lengthItems = this.question.options.length;
            }

        }
    }



    onChange(questonId: string, optionId: string, isChecked: boolean, index: any) {
        const checkboxquestion = this.question as CheckboxQuestion;
        const option = checkboxquestion.options.find((x: ControlOptionModel) => x.id === optionId);
        option.label = isChecked;
        // const answer: any = 'answer';
        const checkBoxArray = <FormArray>this.form.get(this.pageId).get(questonId);
        const checkBoxControl = checkBoxArray.controls['answer'];
        console.log(this.question);
        if (isChecked) {
            checkBoxControl.push(new FormControl(optionId));
        } else {
            index = checkBoxControl.controls.findIndex((x: any) => x.value === optionId);
            checkBoxControl.removeAt(index);
        }
    }

    Setfocus(id: string) {

        // console.log('id.target.value');
        // const vv = this.rows as any;
        // this.rows.last.nativeElement.focus();
        // const val = this.form.controls[id].get('rows').get('id_question2').get('label');

        // this.rows.nativeElement.focus();
        // this.rows.first().nativeElement.focus();

    }



    addItem(mass: any[]) {
        const item = new ControlOptionModel(GUID.getNewGUIDString(), '', 'your text...', 1);

        this.lengthItems = mass.length;
        mass.push(item);
        this.isChangeRow = false;
    }

    addRow(mass: any, questionId: string) {
        const val = this.form.get(this.pageId).get(questionId).get('rows') as FormGroup;
        const group: any = {};
        const key = GUID.getNewGUIDString();
        val.addControl(key, this.answerControlService.addItemAnswer(group, this.question.required));
        const item = new ControlOptionModel(key, '', 'your text...', 1);

        this.lengthRows = mass.length;
        mass.push(item);
        this.isChangeRow = true;
    }


    deleteRow(control: any, index: any, mass: ControlOptionModel[]) {

        if (mass.length > 1) {
            mass.splice(index, 1);
            const contrls = this.form.get(this.pageId).get(this.question.id).get('rows') as FormGroup;
            contrls.removeControl(control.id);
            this.lengthRows = mass.length;
            this.isChangeRow = true;
        }

    }

    deleteItem(index: any, mass: ControlOptionModel[]) {
        if (mass.length > 1) {
            mass.splice(index, 1);
            this.lengthItems = mass.length;
            this.isChangeRow = false;
        }
    }



    ngAfterViewInit() {

        this.sub = this.rows.changes.subscribe((resp: any) => {

            if (this.rows.length > this.lengthRows && this.isChangeRow) {
                this.rows.last().nativeElement.focus();
            }
        });

        this.sub = this.cols.changes.subscribe((resp: any) => {

            if (this.cols.length > this.lengthItems && !this.isChangeRow) {
                this.cols.last().nativeElement.focus();
            }
        });

    }


    // memory leak avoidance
    ngOnDestroy() {
        this.sub.unsubscribe();
    }






    // onChange(key: string, label: string, isChecked: boolean) {
    //     console.log(key);
    //     const checkBoxArray = <FormArray>this.form.controls[key];
    //     let checkBoxControl = checkBoxArray.controls['answer'];
    //     checkBoxControl = this.fb.array([]);
    //     console.log(checkBoxArray);
    //     if (isChecked) {
    //       checkBoxControl.push(new FormControl(label));
    //     } else {
    //       const index = checkBoxControl.controls.findIndex(x => x.value === label)
    //       checkBoxControl.removeAt(index);
    //     }
    //   }



    onChangeGridRadio(itemRowLabel: any, itemCol: any, key: string, label: string, isChecked: boolean) {
        const radioArray = <FormArray>this.form.get(this.pageId).get(key);
        const answer = 'answer';
        const answerControl = radioArray.get(answer);
        const group: any = {};



        group['row'] = this.fb.group({
            'id': new FormControl(itemRowLabel.id),
            'label': new FormControl(itemRowLabel.value)
        });
        group['col'] = this.fb.group({
            'id': new FormControl(itemCol.id),
            'label': new FormControl(itemCol.value)
        });


        const item2 = answerControl.controls.findIndex((x: any) => x.get('row').get('id').value === itemRowLabel.id);
        const needcontrol = answerControl.get(item2);

        if (!!needcontrol) {
            answerControl.removeAt(item2);
        }


        // let index = answerControl.controls.findIndex(x => x.id == itemRowLabel.id)
        // // let index = group.controls.findIndex(x => x.id == itemRowLabel.id)
        // answerControl.removeAt(this.lastSelectRowId);

        const formGroup = new FormGroup(group);
        answerControl.push(formGroup);
        this.lastSelectRowId = formGroup;



        console.log('000000000000000000000000000');
        console.log(radioArray);
        console.log('000000000000000000000000000');

    }

    ngAfterContentChecked() {

        this.cdr.detectChanges();
    }
}
