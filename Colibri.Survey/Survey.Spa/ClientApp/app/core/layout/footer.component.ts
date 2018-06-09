import { Component } from '@angular/core';

@Component({
    selector: 'cmp-footer-layout',
    templateUrl: './footer.component.html',
    styleUrls: ['./footer.component.scss'],
})
export class FooterLayoutComponent {
    year: number;
    constructor() {
        this.year = (new Date()).getFullYear();
    }
}
