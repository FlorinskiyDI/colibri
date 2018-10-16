import { Component, Input, ElementRef, ViewEncapsulation, OnInit, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { QuestionTransferService } from 'shared/transfers/question-transfer.service';
import { GUID } from 'shared/helpers/guide-type.helper';
import { FormGroup } from '@angular/forms';
import { applyDrag, generateItems } from './utils';




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

    constructor(
        private questionTransferService: QuestionTransferService,
        private elementRef: ElementRef,
        private changeDetectorRef: ChangeDetectorRef
    ) {

    }

    ngOnInit() {
        this.selectItem = this.pageId;
        this.carousel = this.elementRef.nativeElement.querySelector('.carousel');
        this.carouselWrapper = this.elementRef.nativeElement.querySelector('.carousel-wrapper');
        console.log('1111111111111111111');
        console.log('1111111111111111111');

        console.log(this.pagingList);

        console.log('1111111111111111111');
        console.log('1111111111111111111');

    }

    selectPage(item: any) {
        this.questionTransferService.setSelectedPage(item.id);
        this.selectItem = item.id;
    }

    items = generateItems(15, (i: any) => ({ data: 'Draggable ' + i }));

    onDrop(dropResult: any) {
        this.items = applyDrag(this.items, dropResult);
    }
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
