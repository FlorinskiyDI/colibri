import { Component, ViewEncapsulation } from '@angular/core';

// import { QuestionBase } from '../../../shared/Models/form-builder/question-base.model';

import { ControTypes } from '../../../shared/constants/control-types.constant';
import { AvailableQuestions } from '../../../shared/models/form-builder/form-control/available-question.model';

import { QuestionService } from '../../../shared/services/api/question.service';
import { QuestionControlService } from '../../../shared/services/question-control.service';

import { QuestionTransferService } from '../../../shared/transfers/question-transfer.service';

@Component({
    selector: 'survey-builder-cmp',
    templateUrl: './survey-builder.component.html',
    styleUrls: [
        './survey-builder.component.scss'
    ],
    encapsulation: ViewEncapsulation.None,
    providers: [QuestionService, QuestionControlService]
})
export class SurveyBuilderComponent {

    widgets: Array<object>;
    questions: any[];
    newquestion: any;
    questionOption: any = {};

    temporaryAnser: any;
    // availableQuestions: string[] = [
    //     ControTypes.checkbox,
    //     ControTypes.dropdown,
    //     ControTypes.gridRadio,
    //     ControTypes.radio,
    //     ControTypes.textarea,
    //     ControTypes.textbox
    // ];

    availableQuestions: Array<AvailableQuestions> = [

        new AvailableQuestions(
            ControTypes.textbox, 'Textbox', 1, 'dropZonesName1', 'Textbox description', 'fa-font'),
        new AvailableQuestions(ControTypes.textarea, 'Textarea', 2, 'dropZonesName2', 'Texarea description', 'fa-text-width'),
        new AvailableQuestions(ControTypes.radio, 'Radiogroup', 3, 'dropZonesName3', 'Radio group description', 'fa-dot-circle-o'),
        new AvailableQuestions(ControTypes.checkbox, 'Checkbox', 4, 'dropZonesName4', 'Checkbox description', 'fa-check-square-o'),
        new AvailableQuestions(ControTypes.dropdown, 'Dropdown', 5, 'dropZonesName5', 'Dropdown description', 'fa-indent'),
        new AvailableQuestions(ControTypes.gridRadio, 'Grid (single choice)', 6, 'dropZonesName6', 'grid description', 'fa-table'),


    ];

    availableQuestionsOption: any;








    listOne: Array<string> = ['Coffee', 'Orange Juice', 'Red Wine', 'Unhealty drink!', 'Water'];
    listTeamOne: Array<string> = [];
    listTeamOne5: Array<string> = [];
    availableProducts: Array<Product> = [];
    shoppingBasket: Array<Product> = [];

    constructor(
        private questionTransferService: QuestionTransferService,
        public questionService: QuestionService,
        public questionControlService: QuestionControlService
    ) {


        this.availableQuestionsOption = {
            allowquestion: ['dropZonesName1', 'dropZonesName2', 'dropZonesName3', 'dropZonesName4', 'dropZonesName5', 'dropZonesName6'],
            availableQuestions: [
                { type: ControTypes.textbox, name: 'Textbox', order: 1, dropZonesName: 'dropZonesName1', description: 'Textbox description', icon: 'fa-font' } as AvailableQuestions,
                { type: ControTypes.textarea, name: 'Textarea', order: 2, dropZonesName: 'dropZonesName2', description: 'Texarea description', icon: 'fa-text-width' } as AvailableQuestions,
                { type: ControTypes.radio, name: 'Radiogroup', order: 3, dropZonesName: 'dropZonesName3', description: 'Radio group description', icon: 'fa-dot-circle-o' } as AvailableQuestions,
                { type: ControTypes.checkbox, name: 'Checkbox', order: 4, dropZonesName: 'dropZonesName4', description: 'Checkbox description', icon: 'fa-check-square-o' } as AvailableQuestions,
                { type: ControTypes.dropdown, name: 'Dropdown', order: 5, dropZonesName: 'dropZonesName5', description: 'Dropdown description', icon: 'fa-indent' } as AvailableQuestions,
                { type: ControTypes.gridRadio, name: 'Grid (single choice)', order: 6, dropZonesName: 'dropZonesName6', description: 'grid description', icon: 'fa-table' } as AvailableQuestions
            ],
        };
        this.listTeamOne5.push('111111111111112');
        // this.listTeamOne.push('11111111111111');

        this.questions = questionService.getQuestions();
        this.availableProducts.push(new Product('Blue Shoes', 3, 35));
        this.availableProducts.push(new Product('Good Jacket', 1, 90));
        this.availableProducts.push(new Product('Red Shirt', 5, 12));
        this.availableProducts.push(new Product('Blue Jeans', 4, 60));
    }

    ngOnInit() {
        this.widgets = this.generateWidgets(11);
    }
    generateWidgets = (num: number) => {
        const result = [];
        for (let i = 0; i < num; i++) {
            result.push({ title: 'Page ' + ( i + 1 ) + '*' });
        }
        return result;
    }

    getQuestonDropzones() {
        // console.log('101010101010101010101010101010101010101010101010101010101010101010101');
        // console.log(this.availableQuestions);
        // console.log('101010101010101010101010101010101010101010101010101010101010101010101');
        const dropZoneList: any[] = [];
        this.availableQuestions.forEach(val => {
            dropZoneList.push(val.dropZonesName);
        });
        return dropZoneList;
    }



    removeDragQuestion(question: any) {
        this.questionTransferService.setDropQuestion(question);
    }


    onDragEnd8(event: any) {
        console.log('88888888888888888888899999999999999999999999');
        console.log(event);
        console.log('88888888888888888888899999999999999999999999');
    }




    orderedProduct($event: any) {
        // const orderedProduct: Product = $event.dragData;
        // orderedProduct.quantity--;
    }

 

    dragOperation = false;

    containers: Array<Container> = [
        new Container(1, 'Container 1', [new Widget('1'), new Widget('2')]),
        new Container(2, 'Container 2', [new Widget('3'), new Widget('4')]),
        new Container(3, 'Container 3', [new Widget('5'), new Widget('6')])
    ];

    // widgets: Array<Widget> = [];
    // addTo($event: any) {
    //     if ($event) {
    //         this.widgets.push($event.dragData);
    //         this.listTeamOne.push($event.dragData);
    //         console.log('777777777777777777777777777777777777777777777');
    //         console.log('777777777777777777777777777777777777777777777');
    //         console.log('777777777777777777777777777777777777777777777');
    //     }
    // }

    listBoxers: Array<string> = ['Sugar Ray Robinson',
        'Muhammad Ali', 'George Foreman', 'Joe Frazier', 'Jake LaMotta', 'Joe Louis', 'Jack Dempsey', 'Rocky Marciano', 'Mike Tyson', 'Oscar De La Hoya'];

    listTeamTwo: Array<string> = [];






    sourceList: Widget[] = [
        new Widget('1'), new Widget('2'),
        new Widget('3'), new Widget('4'),
        new Widget('5'), new Widget('6')
    ];

    targetList1: Widget[] = [];
    targetList2: Widget[] = [];

    addTo2($event: any) {
        this.targetList2.push($event.dragData);
    }

    addTo1($event: any) {
        this.targetList1.push($event.dragData);

    }
}

class Product {
    constructor(public name: string, public quantity: number, public cost: number) { }
}


class Container {
    constructor(public id: number, public name: string, public widgets: Array<Widget>) { }
}

class Widget {
    constructor(public name: string) { }
}



// evailabal question list
// class AvailableQuestions {
//     constructor(public name: string, public order: number, public dropZonesName: string) { }
// }
