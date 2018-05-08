import { Component, Input, ElementRef, ViewEncapsulation, EventEmitter, Output, OnInit, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { QuestionTransferService } from '../../../../shared/transfers/question-transfer.service';
@Component({
    selector: 'paging',
    templateUrl: './paging.component.html',
    styleUrls: [
        './paging.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
})
export class PagingComponent implements OnInit, AfterViewChecked {

    @Output() pageId: EventEmitter<any> = new EventEmitter<any>();
    @Input() items: any[] = [];

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
    ii = 0;
    selectItem: string;
    constructor(
        private questionTransferService: QuestionTransferService,
        private elementRef: ElementRef,
        private changeDetectorRef: ChangeDetectorRef
    ) { }

    ngOnInit() {

        this.selectItem = this.items[0].id;
        this.carousel = this.elementRef.nativeElement.querySelector('.carousel');
        this.carouselWrapper = this.elementRef.nativeElement.querySelector('.carousel-wrapper');
    }

    selectPage(item: any) {

        this.pageId.emit(item.id);
        this.selectItem = item.id;
    }

    renderBatches = () => {
        const itemWidth = 130;

        // we split the items in batches
        this.elementsPerSlide = Math.floor(this.carouselWrapperWidth / itemWidth);
        this.elementsPerSlide = this.items.length;
        this.batches = this.chunk(this.items, this.elementsPerSlide);

        // The carousel is 100 * ng batches wide
        this.carousel.style.width = 100 * this.batches.length + '%';
        this.carousel.style.width = 130 * this.items.length + 'px';
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
        const value = { title: 'Page ' + 30 + this.ii + '*' };
        this.ii++;
        this.items.push(value);
        console.log(this.items);

        this.renderBatches();
        this.setBatchSize();
        this.carousel.style.transform = 'translateX(' + -85 + '%)';
        this.translation = -85;
        this.selectItem = value.title;

    }

    deletePage(id: string, index: number, event: any) {
        this.questionTransferService.setdeletePageId({id: id, index: index});
        debugger
        this.items.splice(index, 1);
        this.batches[0].splice(index, 1);
        // event.preventDefault();
        // event.stopPropagation();
    }




    slideForward = (direction: number) => {
        // if (this.carousel.offsetWidth <= (this.items.length * 130)) {
        this.currentItem = this.currentItem - direction;
        this.translation = -33.333333333333336 / 5 + this.count;
        this.count = this.translation;

        if (this.count <= -100) {
            this.translation = -100;
        }
        this.carousel.style.transform = 'translateX(' + this.translation + '%)';
        console.log('move paginator page in right');
        // }
    }

    slideBackward = (direction: number) => {
        this.currentItem = this.currentItem - direction;
        this.translation = this.count + (33.333333333333336 / 10);
        this.count = this.translation;
        if (this.count >= 0) {
            this.translation = 0;
        }
        this.carousel.style.transform = 'translateX(' + this.translation + '%)';
        console.log('move paginator page in left');
    }
    chunk = (arr: any, n: any) => {
        const val = arr.slice(0, (arr.length + n - 1) / n | 0).map((c: any, i: any) => arr.slice(n * i, n * i + n));
        return val;
    }
}
