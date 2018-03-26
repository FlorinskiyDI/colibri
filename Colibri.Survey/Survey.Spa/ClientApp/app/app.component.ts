import { Component, OnInit, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'app',
    animations: [ ],
    templateUrl: './app.component.html',
    styleUrls: [ '../styles/custom.shared.scss' ],
    encapsulation: ViewEncapsulation.None,
    providers: [ ]

})
export class AppComponent implements OnInit {
    constructor( ) { }
    ngOnInit() { }
}
