import { Component, Input, OnInit, EventEmitter, Output, AfterContentChecked, ChangeDetectorRef } from '@angular/core';
import { FormGroup } from '@angular/forms';

// import { applyDrag } from '../utils/utilse.service';
// models
import { ControTypes } from 'shared/constants/control-types.constant';
import { QuestionBase } from 'shared/models/form-builder/question-base.model';
import { PageModel } from 'shared/models/form-builder/page.model';

// services
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { QuestionService } from '../services/builder.service';
import { ControStates } from 'shared/constants/control-states.constant';

// import { ContainerComponent, DraggableComponent, IDropResult } from 'ngx-smooth-dnd';



@Component({
    selector: 'form-builder',
    templateUrl: './form-builder.component.html',
    styleUrls: ['./form-builder.component.scss'],
    providers: [QuestionService],
})
export class FormBuilderComponent implements OnInit, AfterContentChecked {

    @Input() templateOptions: any;
    @Input() page: PageModel = new PageModel();
    @Input() pagingList: any[];
    @Input() numericPageFrom: number;
    @Output() formState = new EventEmitter<any>();

    formPage: FormGroup;
    selectQuestion: string;
    static deleteQuestionList: string[] = [];

    onDrop(dropResult: any) {

        // update item list according to the @dropResult
        this.applyDrag(this.page.questions, dropResult);
    }

    applyDrag = (arr: any, dragResult: any) => {
        const { removedIndex, addedIndex, payload } = dragResult;

        if (removedIndex === null && addedIndex === null) {
            return arr;
        }

        let itemToAdd = payload;
        if (removedIndex !== null) {
            itemToAdd = this.page.questions.splice(removedIndex, 1)[0];
        }
        if (addedIndex !== null) {

            this.addQuestion(itemToAdd, addedIndex);

        }


    }



    constructor(
        private cdr: ChangeDetectorRef,
        private questionTransferService: QuestionTransferService,
        public questionService: QuestionService,
    ) {
        this.questionTransferService.getFormPage().subscribe((page: any) => { // updata formbuild after select page
            if (page) {
                this.formPage.addControl(page.id, this.questionService.getFormPageGroup(page));
            }
        });

        this.questionTransferService.getQuestionForDelete().subscribe((data: any) => { // check drag control if lost focus without need area

            this.page.questions.forEach((item: any, index: number) => {
                const value = item as QuestionBase<any>;
                if (!value.id) {
                    this.page.questions.splice(0, 1);
                }
            });
        });

        this.questionTransferService.getdeletePageId().subscribe((data: any) => {
            this.formPage.removeControl(data.id);
        });


        this.questionTransferService.getDeleteDragQuestion().subscribe((data: any) => {

            FormBuilderComponent.deleteQuestionList.push(data.id);

            this.page.questions.splice(data.order, 1);

            const formQuestions = this.formPage.controls[this.page.id] as FormGroup;
            formQuestions.removeControl(data.id);
            // this.page.questions.sort((a, b) => a.order - b.order);
            this.sortQuestionByIndex();

            const updateQuestioRange = this.page.questions.slice(data.order, this.page.questions.length); // get question range (max, min) for make resortable
            updateQuestioRange.forEach((item: any) => {
                item.state = item.state !== ControStates.created ? ControStates.updated : item.state;
            });
        });



        this.questionTransferService.getDataForChangeQuestion().subscribe((data: any) => {
            this.page.questions[data.object.order] = data.object;
            const val = this.formPage.controls[this.page.id] as FormGroup;
            val.setControl(data.object.id, data.control);

            this.questionTransferService.setQuestionOption(
                {
                    question: this.page.questions[data.object.order],
                    control: this.formPage.get(this.page.id).get(data.object.id)
                }
            );
        });

        this.getQuestionPayload = this.getQuestionPayload.bind(this);
    }


    ngOnInit() {
        const page: any = {};
        page[this.page.id] = this.questionService.getFormPageGroup(this.page);
        this.formPage = new FormGroup(page);
        this.formPage.valueChanges.subscribe(form => {
            this.formState.emit(this.formPage.valid);
        });
    }


    getQuestionPayload(index: any) {
        return this.page.questions[index];
    }


    setQuestionOption(question: any, checked: boolean) {
        this.selectQuestion = question.id;
        if (checked) {
            this.questionTransferService.setQuestionOption(
                {
                    question: question,
                    control: this.formPage.get(this.page.id).get(question.id)
                }
            );
        } else {
            this.questionTransferService.setQuestionOption(null);
        }
    }

    getFormQuestion(id: string): any {
        const formQuestion = this.formPage.controls[this.page.id].get('questions').get(id);

        return formQuestion;
    }


    sortQuestion(event: any, index: number) {

        const minIndex = event.order > index ? index : event.order;
        const maxIndex = event.order > index ? event.order : index;

        const updateQuestioRange = this.page.questions.slice(minIndex, maxIndex + 1); // get question range (max, min) for make resortable
        updateQuestioRange.forEach((item: any) => {
            item.state = item.state !== ControStates.created ? ControStates.updated : item.state;
        });
        // this.page.questions.sort((a, b) => a.order - b.order);
        this.sortQuestionByIndex();
    }


    sortQuestionByIndex() {
        this.page.questions.forEach(x => {

            const indexOf = this.page.questions.indexOf(x);
            x.order = indexOf;
        });
    }

    addQuestion(object: any, index: number) {

        // this.page.questions.splice(index, 1); // remove AvailableQuestions object
        let question: any;
        const isAddItem = object.name;
        
        if (isAddItem === undefined) {
            question = object;
            question.order = index;

        } else {
            question = this.getQuestionByType(object.type, index);

        }

        question.state = ControStates.created;
        const group: any = {};

        const dataPage = this.formPage.controls[this.page.id] as FormGroup;
        dataPage.addControl(question.id, this.questionService.addTypeAnswer(question, group));

        this.page.questions.splice(index, 0, question);
        console.log(this.page.questions);

        const updateQuestioRange = this.page.questions.slice(index, this.page.questions.length); // get question range (max, min) for make resortable
        updateQuestioRange.forEach((item: any) => {
            item.state = item.state !== ControStates.created ? ControStates.updated : item.state;
        });

        this.page.questions.sort((a, b) => a.order - b.order);
        this.sortQuestionByIndex();
    }


    getQuestionByType(value: any, index: any) {
        switch (value) {
            case ControTypes.textbox: {
                return this.questionService.getTextboxControl(index);
            }
            case ControTypes.textarea: {
                return this.questionService.getTextareaControl(index);
            }
            case ControTypes.radio: {
                return this.questionService.getRadioButtonControl(index);
            }
            case ControTypes.checkbox: {
                return this.questionService.getCheckBoxControl(index);
            }
            case ControTypes.dropdown: {
                return this.questionService.getDropdownControl(index);
            }
            case ControTypes.gridRadio: {
                return this.questionService.getGridRadioControl(index);
            }
            default: {
                console.log('Invalid choice');
                return null;
            }
        }
    }



    ngAfterContentChecked() {
        this.cdr.detectChanges();
    }
}
