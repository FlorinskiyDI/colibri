import { Component } from '@angular/core';

@Component({
    selector: 'survey-builder-cmp',
    templateUrl: './survey-builder.component.html',
    styleUrls: [
        './survey-builder.component.scss'
    ]
})
export class SurveyBuilderComponent {
    // listOne: Array<string> = ['Coffee', 'Orange Juice', 'Red Wine', 'Unhealty drink!', 'Water'];
    // listTeamOne: Array<string> = [];
    // availableProducts: Array<Product> = [];
    // shoppingBasket: Array<Product> = [];

    // constructor() {
    //     this.availableProducts.push(new Product('Blue Shoes', 3, 35));
    //     this.availableProducts.push(new Product('Good Jacket', 1, 90));
    //     this.availableProducts.push(new Product('Red Shirt', 5, 12));
    //     this.availableProducts.push(new Product('Blue Jeans', 4, 60));
    // }

    // orderedProduct($event: any) {
    //     const orderedProduct: Product = $event.dragData;
    //     orderedProduct.quantity--;
    // }

    // addToBasket($event: any) {
    //     console.log($event);
    //     this.listTeamOne.push($event);
    //     console.log(this.listTeamOne);
    // }

    // totalCost(): number {
    //     return 6;
    // }

    // changeSort(event: any) {
    //     console.log(event);
    // }


    // checkOnMouseUp(event: any) {
    //     console.log('eventeventeventeventeventeventevent');
    //     console.log(event);
    //     console.log('eventeventeventeventeventeventevent');
    // }
    // transferDataSuccess($event: any) {
    //     console.log($event);
    //     this.listTeamOne.push($event);
    // }

}

// class Product {
//     constructor(public name: string, public quantity: number, public cost: number) { }
// }
