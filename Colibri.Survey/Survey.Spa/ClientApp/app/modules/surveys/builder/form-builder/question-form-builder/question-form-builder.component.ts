import {
    Component, Input, AfterContentChecked, ChangeDetectorRef, AfterViewInit, QueryList,
    OnDestroy, ChangeDetectionStrategy, ViewChildren, Output, EventEmitter
} from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

import { Subscription } from 'rxjs/Subscription';
import { GUID } from '../../../../../shared/helpers/guide-type.helper';
import { ControlOptionModel } from '../../../../../shared/models/form-builder/form-control/control-option.model';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { ControStates } from 'shared/constants/control-states.constant';

@Component({
    selector: 'question-form-builder',
    templateUrl: './question-form-builder.component.html',
    styleUrls: ['./question-form-builder.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})


export class QuestionFormBuilderComponent implements AfterContentChecked, OnDestroy, AfterViewInit {

    @ViewChildren('inputRow') rows: QueryList<any>;
    @ViewChildren('inputCol') cols: QueryList<any>;

    @Input() isEditState: boolean;
    @Input() question: any;
    @Input() formPage: FormGroup;
    @Input() pageId: any;

    // @Output() removeQuestionId = new EventEmitter<string>();

    isChangeRow = true;
    lengthRows = 0;
    lengthItems = 0;

    private sub = new Subscription();

    constructor(
        private fb: FormBuilder,
        private cdr: ChangeDetectorRef,
        private questionTransferService: QuestionTransferService,
    ) { }


    ngOnInit() { }


    ngOnDestroy() {
        this.sub.unsubscribe();
    }


    ngAfterContentChecked() {
        this.cdr.detectChanges();
    }


    ngAfterViewInit() {
        this.sub = this.rows.changes.subscribe((resp: any) => {
            if (this.rows.length > this.lengthRows && this.isChangeRow) {
                this.rows.last.nativeElement.focus();
            }
        });
        this.sub = this.cols.changes.subscribe((resp: any) => {
            if (this.cols.length > this.lengthItems && !this.isChangeRow) {
                this.cols.last.nativeElement.focus();
            }
        });
    }


    addItem(mass: any[], questionId: string) {
        
        const optionId = GUID.getNewGUIDString();
        const page = this.formPage.controls[this.pageId].get(questionId).get('options') as FormGroup;
        page.addControl(optionId, this.fb.group({
            'name': new FormControl('Some value...', Validators.required)
        }));
        const item = new ControlOptionModel(optionId, '', 'your text...', 1);
        this.lengthItems = mass.length;
        mass.push(item);
        this.isChangeRow = false;

        this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;
    }


    deleteItem(index: any, mass: ControlOptionModel[], itemId: string, questionId: string) {
        if (mass.length > 1) {
            const page = this.formPage.controls[this.pageId].get(questionId).get('options') as FormGroup;
            page.removeControl(itemId);
            mass.splice(index, 1);
            this.lengthItems = mass.length;
            this.isChangeRow = false;

            this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;
        }
    }


    addCol(mass: any[], questionId: string) {
        const colId = GUID.getNewGUIDString();
        const colsList = this.formPage.controls[this.pageId].get(questionId).get('cols') as FormGroup;
        colsList.addControl(colId, this.fb.group({
            'name': new FormControl('Some value...', Validators.required)
        }));
        const item = new ControlOptionModel(colId, '', 'your text...', 1);
        this.lengthItems = mass.length;
        mass.push(item);
        this.isChangeRow = false;

        this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;
    }


    deleteCol(index: any, question: any, colId: string) {
        if (question.grid.cols.length > 1) {
            const colsList = this.formPage.controls[this.pageId].get(question.id).get('cols') as FormGroup;
            colsList.removeControl(colId);
            question.grid.cols.splice(index, 1);
            this.lengthItems = question.grid.cols.length;
            this.isChangeRow = false;

            this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;

        }
    }


    addRow(mass: any[], questionId: string) {
        const rowId = GUID.getNewGUIDString();

        const rowsList = this.formPage.controls[this.pageId].get(questionId).get('rows') as FormGroup;
        rowsList.addControl(rowId, this.fb.group({
            'name': new FormControl('Some value...', Validators.required)
        }));
        const item = new ControlOptionModel(rowId, '', 'your text...', 1);
        this.lengthRows = mass.length;
        mass.push(item);
        this.isChangeRow = true;

        this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;

    }


    deleteRow(index: any, question: any, rowId: string) {
        if (question.grid.rows.length > 1) {
            const rowsList = this.formPage.controls[this.pageId].get(question.id).get('rows') as FormGroup;
            rowsList.removeControl(rowId);
            question.grid.rows.splice(index, 1);
            this.lengthRows = question.grid.rows.length;
            this.isChangeRow = true;

            this.question.state = this.question.state !== ControStates.created ? ControStates.updated : ControStates.created;

        }
    }


    removeQuestion(question: any) {
        this.questionTransferService.setDeleteDragQuestion(question);
        // console.log(id);
    }
}
