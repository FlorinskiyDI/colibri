import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { MatSidenav } from '@angular/material';
import { FormControl } from '@angular/forms';

@Component({
    selector: 'cmp-sidebar',
    templateUrl: 'sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {
    @ViewChild('sidenav') sidenav: MatSidenav;
    @Input() eventSidebarToggle: Observable<any>;
    private subscriberSidebarToggle: any;
    mode = new FormControl('over');
    constructor() {
    }
    ngOnInit() {
        this.subscriberSidebarToggle = this.eventSidebarToggle.subscribe(() => this._toggleOpened());
    }

    ngOnDestroy() {
        this.subscriberSidebarToggle.unsubscribe();
    }
    _toggleOpened(): void {
        this.sidenav.toggle();
        console.log('!!!!');
    }

    mouseEnter(data: any, ...args: any[]) {
        data.openMenu();
        args.forEach((item: any) => {
            item.closeMenu();
        });
    }
}


// import { AfterContentInit, Component, Input, ViewChild } from '@angular/core';
// import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
// import { MatSidenav } from '@angular/material';
// import { Subject } from 'rxjs/Subject';
// import { Observable } from 'rxjs/Observable';
// import { map } from 'rxjs/operators';

// @ Component({
//   selector: 'cmp-sidebar',
//   templateUrl: './sidebar.component.html',
//   styles: []
// })
// export class SidebarComponent implements AfterContentInit {
//   isHandset$: Observable<boolean> = this.breakpointObserver.observe(Breakpoints.Handset)
//     .pipe(map(result => result.matches));

//   @Input() openedSubject: Subject<boolean>;
//   @ViewChild('sidenav') sidenav: MatSidenav;

//   constructor(private breakpointObserver: BreakpointObserver) {
//   }

//   ngAfterContentInit() {
//     this.openedSubject.subscribe( c => {
//         console.log('!!!!!!');
//         this.openedSubject.next(!this.sidenav.opened);
//     }
//     //   keepOpen => this.sidenav[keepOpen ? 'open' : 'close']()
//     );
//   }

// //   toggle() {
// //     this.openedSubject.next(!this.sidenav.opened);
// //   }

// }
