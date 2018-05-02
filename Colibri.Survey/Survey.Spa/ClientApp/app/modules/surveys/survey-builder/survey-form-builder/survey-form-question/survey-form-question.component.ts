import {
    Component, Input, AfterContentChecked,
    ViewChildren, QueryList, ChangeDetectorRef, AfterViewInit, OnDestroy,
    ChangeDetectionStrategy, ViewChild, ElementRef
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

    isDeleteRow = false;
    valueTest: any = null;
    lengthRows: number;
    lengthItem: number;
    lastSelectRowId: any;
    isRowsChange = true;
    // @ViewChild('focusItem') rows: ElementRef;
    @ViewChildren('inputRow') rows: QueryList<any>;
    @ViewChildren('inputItem') items: QueryList<any>;
    @Input() question: QuestionBase<any>;
    @Input() form: FormGroup;
    @Input() isEditQuestion: boolean;

    private sub1 = new Subscription();
    private sub2 = new Subscription();

    get isValid() {
        return this.form.controls[this.question.id].valid;
    }
    get isDirty() { return this.form.controls[this.question.id].dirty; }


    constructor(
        private answerControlService: AnswerControlService,
        private cdr: ChangeDetectorRef,
        private fb: FormBuilder) {


    }

    ngOnInit() {
        debugger
        if (this.question) {
            debugger
            this.lengthRows = this.question.grid.rows.length;
            this.lengthItem = this.question.grid.cols.length;
            console.log(this.lengthRows);
            console.log(this.lengthItem);
            console.log(this.lengthRows);
            console.log(this.lengthItem);
            console.log(this.lengthRows);
            console.log(this.lengthItem);
            console.log(this.lengthRows);
            console.log(this.lengthItem);
        } else {
            this.lengthItem = this.question.options.length;
        }
    }


    onChange(questonId: string, optionId: string, isChecked: boolean, index: number) {
        const checkboxquestion = this.question as CheckboxQuestion;
        const option = checkboxquestion.options.find((x: ControlOptionModel) => x.id === optionId);
        option.label = isChecked;
        // const answer: any = 'answer';
        const checkBoxArray = <FormArray>this.form.controls[questonId];
        const checkBoxControl = checkBoxArray.controls['answer'];
        console.log(this.question);
        if (isChecked) {
            checkBoxControl.push(new FormControl(optionId));
        } else {
            index = checkBoxControl.controls.findIndex((x: any) => x.value === optionId);
            checkBoxControl.removeAt(index);
        }
    }





    addItem(mass: any[]) {
        this.isRowsChange = false;
        const item = new ControlOptionModel(GUID.getNewGUIDString(), '', 'your text...', 1);
        this.lengthItem = mass.length;
        mass.push(item);
    }

    addRow(mass: any, questionId: string) {
        this.isRowsChange = true;
        const val = this.form.controls[questionId].get('rows') as FormGroup;
        const group: any = {};
        const key = GUID.getNewGUIDString();
        val.addControl(key, this.answerControlService.addItemAnswer(group));
        const item = new ControlOptionModel(key, '', 'your text...', 1);
        this.lengthRows = mass.length;
        mass.push(item);
    }

    ngAfterViewInit() {

        console.log(this.isRowsChange);
        this.sub1 = this.rows.changes.subscribe(resp => {
            debugger
            if (this.rows.length > this.lengthRows && this.rows.length !== this.lengthRows) {
                this.rows.last.nativeElement.focus();
            }
        });

        this.sub2 = this.items.changes.subscribe(resp => {
            debugger
            if (this.items.length > this.lengthItem && this.items.length !== this.lengthItem) {
                this.items.last.nativeElement.focus();
            }
        });




    }


    // memory leak avoidance
    ngOnDestroy() {
        this.sub1.unsubscribe();
        this.sub2.unsubscribe();
    }

    addCol(mass: any) {
        const item = new ControlOptionModel(GUID.getNewGUIDString(), '', 'your text...', 1);
        mass.push(item);
    }


    deleteRow(index: number, mass: ControlOptionModel[]) {
        this.isRowsChange = true;
        mass.splice(index, 1);
        this.lengthRows = mass.length;
    }
    deleteItem(index: number, mass: ControlOptionModel[]) {
        this.isRowsChange = false;
        mass.splice(index, 1);
        this.lengthItem = mass.length;
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
        const radioArray = <FormArray>this.form.controls[key];
        const answer = 'answer';
        const answerControl = radioArray.controls[answer];
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


        // let index = answerControl.controls.findIndex(x => x.id == itemRowLabel.id)
        // // let index = group.controls.findIndex(x => x.id == itemRowLabel.id)
        // answerControl.removeAt(this.lastSelectRowId);

        const formGroup = new FormGroup(group);
        answerControl.push(formGroup);
        this.lastSelectRowId = formGroup;

        // answerControl = [];
        //   if (isChecked && this.lastSelectRowId == '' || this.lastSelectRowId == itemRowLabel.id ) {
        //     console.log('workkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk');

        //   } else {

        //     // let index = answerControl.controls.findIndex(x => x.id == itemCol.id)
        //     // checkBoxControl.re.removeAt(index);
        //   }


        console.log('000000000000000000000000000');
        console.log(radioArray);
        console.log('000000000000000000000000000');

    }

    ngAfterContentChecked() {

        this.cdr.detectChanges();
    }
}
