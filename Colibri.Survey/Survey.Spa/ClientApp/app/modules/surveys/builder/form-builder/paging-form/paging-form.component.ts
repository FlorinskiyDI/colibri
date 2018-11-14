import { Component, Input, ElementRef, ViewEncapsulation, OnInit, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { GUID } from 'shared/helpers/guide-type.helper';
import { FormGroup } from '@angular/forms';
import { IDropResult } from 'ngx-smooth-dnd';




@Component({
    selector: 'paging-form',
    templateUrl: './paging-form.component.html',
    styleUrls: [
        './paging-form.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})
export class PagingFormComponent implements OnInit, AfterViewChecked {

    @Input() formPage: FormGroup;
    @Input() pageId: string;
    @Input() pagingList: any[];

    currentItem = 0;
    carousel: any;
    position = 0;
    increment: any;
    translation = 0;
    elementsPerSlide: number;
    batches: Array<Array<object>> = [];
    carouselWidth: number;
    carouselWrapper: any;
    carouselWrapperWidth: number;
    count = 0;
    selectItem: string;
    selectedPage: any;
    surveyKey = '-surveyKey-';

    dpdPages: any[] = [];
    constructor(
        private questionTransferService: QuestionTransferService,
        private elementRef: ElementRef,
        private changeDetectorRef: ChangeDetectorRef
    ) {

    }



    onDrop(dropResult: IDropResult) {
        // update item list according to the @dropResult
        // this.items = this.applyDrag(this.items, dropResult);
        this.batches[0] = this.applyDrag(this.batches[0], dropResult);
        this.updateDpdList(this.batches[0]);

    }


    updateDpdList(list: any) {
        // update paging-dnd after drop list
        this.dpdPages = [];
        list.forEach((item: any, index: any) => {
            this.dpdPages.push({ label: item.title + ' №' + (index + 1), value: item.id });
            this.dpdPages.unshift({ label: 'SURVEY OPTIONS', value: '-surveyId-' });
        });
    }


    generateItems(count: any, creator: any) {
        const result = [];
        for (let i = 0; i < count; i++) {
            result.push(creator(i));
        }
        return result;
    }


    applyDrag(arr: any, dragResult: any) {
        const { removedIndex, addedIndex, payload } = dragResult;
        if (removedIndex === null && addedIndex === null) {
            return arr;
        }

        const result = [...arr];
        let itemToAdd = payload;

        if (removedIndex !== null) {
            itemToAdd = result.splice(removedIndex, 1)[0];
        }

        if (addedIndex !== null) {
            result.splice(addedIndex, 0, itemToAdd);
        }

        this.questionTransferService.setPagingListForSort(result);

        return result;
    }


    ngOnInit() {
        // this.selectItem = this.pageId;
        this.carousel = this.elementRef.nativeElement.querySelector('.carousel');
        this.carouselWrapper = this.elementRef.nativeElement.querySelector('.carousel-wrapper');
        this.pagingList.forEach((item: any, index: any) => {
            this.dpdPages.push({ label: item.title + ' №' + (index + 1), value: item.id });
        });
        this.dpdPages.unshift({ label: 'SURVEY_OPTIONS', value: this.surveyKey });
        this.selectedPage = this.dpdPages[0].value;
    }


    selectPage(item: any) {
        this.questionTransferService.setSelectedPage(item.id);
        this.selectItem = item.id;
        this.selectedPage = item.id;
    }


    selectDpdPage(event: any) {

        if (event.value === this.surveyKey) {
            this.questionTransferService.setSelectedPage(event.value);
            this.selectItem = event.value;
            console.log('go to home page');
        } else {
            this.questionTransferService.setSelectedPage(event.value);
            this.selectItem = event.value;
        }
    }

    // items = generateItems(15, (i: any) => ({ data: 'Draggable ' + i }));
    // onDrop(dropResult: any) {
    //     this.items = applyDrag(this.items, dropResult);
    // }

    renderBatches = () => {
        const itemWidth = 130;

        this.elementsPerSlide = Math.floor(this.carouselWrapperWidth / itemWidth);
        this.elementsPerSlide = this.pagingList.length;
        this.batches = this.chunk(this.pagingList, this.elementsPerSlide);

        this.carousel.style.width = 100 * this.batches.length + '%';
        this.carousel.style.width = 130 * this.pagingList.length + 'px';
        this.increment = (100 / this.batches.length);
        this.changeDetectorRef.detectChanges();
    }

    setBatchSize = () => {

        const ulElements = this.elementRef.nativeElement.querySelectorAll('ul');
        // Each ul element needs to be 100 / nb batches wide.
        for (let i = 0; i < ulElements.length; i++) {
            ulElements[i].style.width = 100 / this.batches.length + '%';
        }
    }

    // On size changes, recalculate the bacthes
    ngAfterViewChecked() {
        if (this.carouselWrapper.offsetWidth !== 0 && this.carouselWrapper.offsetWidth !== this.carouselWrapperWidth) {
            this.carouselWrapperWidth = this.carouselWrapper.offsetWidth;

            this.renderBatches();
            this.setBatchSize();
        }
    }

    addPage() {
        const pageId = GUID.getNewGUIDString();
        const value = { title: 'Page', id: pageId };

        this.pagingList.push(value);
        this.dpdPages.push({ label: value.title + ' №' + (this.dpdPages.length + 1), value: value.id });
        this.renderBatches();
        this.setBatchSize();
        if (this.pagingList.length > 6) {
            this.carousel.style.transform = 'translateX(' + -10 + '%)';
            this.translation = -10;
        }

        this.selectItem = value.id;
        this.questionTransferService.setPageById(pageId);

    }

    deletePage(id: string, index: number, event: any) {
        this.questionTransferService.setdeletePageId({ id: id, index: index });

        this.pagingList.splice(index, 1);
        if (index > 0) {
            this.selectItem = this.pageId === this.selectItem ? this.pagingList[index - 1].id : this.pagingList[this.pagingList.length - 1].id;

        } else {
            this.selectItem = this.pageId === this.selectItem ? this.pagingList[0].id : this.selectItem;
        }
        this.batches[0].splice(index, 1);
        this.updateDpdList(this.pagingList);
    }



    slideForward = (direction: number) => {
        this.currentItem = this.currentItem - direction;
        this.translation = -33.333333333333336 / 5 + this.count;
        this.count = this.translation;

        if (this.count <= -100) {
            this.translation = -100;
        }
        this.carousel.style.transform = 'translateX(' + this.translation + '%)';
    }

    slideBackward = (direction: number) => {
        this.currentItem = this.currentItem - direction;
        this.translation = this.count + (33.333333333333336 / 10);
        this.count = this.translation;
        if (this.count >= 0) {
            this.translation = 0;
        }
        this.carousel.style.transform = 'translateX(' + this.translation + '%)';

    }
    chunk = (arr: any, n: any) => {
        // tslint:disable-next-line:no-bitwise
        const val = arr.slice(0, (arr.length + n - 1) / n | 0).map((c: any, i: any) => arr.slice(n * i, n * i + n));
        return val;
    }
}
