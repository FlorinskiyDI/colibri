
import { Component } from '@angular/core';

@Component({
    selector: 'main-portal',
    templateUrl: './main-portal.component.html',
    styleUrls: [
        './main-portal.component.scss'
    ],
    // template: `<router-outlet></router-outlet>`
})
export class MainPortalComponent {
    constructor() {
        console.log('work main portal page');
    }


}
