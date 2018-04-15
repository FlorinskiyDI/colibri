import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import 'rxjs/add/operator/distinctUntilChanged';

@Component({
    selector: 'layout-component',
    templateUrl: 'layout.component.html'
})

export class LayoutComponent implements OnInit {
    breadcrumbs$: any;

    constructor(
        private router: Router,
        private activatedRoute: ActivatedRoute
    ) {


    }
    private _opened = false;
    private _modeNum = 1;
    private _positionNum = 0;
    private _dock = true;
    private _closeOnClickOutside = false;
    private _closeOnClickBackdrop = false;
    private _showBackdrop = false;
    private _animate = true;
    private _trapFocus = true;
    private _autoFocus = true;
    private _keyClose = false;
    private _autoCollapseHeight: number | null;
    private _autoCollapseWidth: number | null;

    private _MODES: Array<string> = ['over', 'push', 'slide'];
    private _POSITIONS: Array<string> = ['left', 'right', 'top', 'bottom'];

    togglemenu(event: any) {
        this._opened = !this._opened;
    }

    _toggleOpened(): void {
        this._opened = !this._opened;
    }

    _toggleMode(): void {
        this._modeNum++;

        if (this._modeNum === this._MODES.length) {
            this._modeNum = 0;
        }
    }

    _toggleAutoCollapseHeight(): void {
        this._autoCollapseHeight = this._autoCollapseHeight ? null : 500;
    }

    _toggleAutoCollapseWidth(): void {
        this._autoCollapseWidth = this._autoCollapseWidth ? null : 500;
    }

    _togglePosition(): void {
        this._positionNum++;

        if (this._positionNum === this._POSITIONS.length) {
            this._positionNum = 0;
        }
    }

    _toggleDock(): void {
        this._dock = !this._dock;
    }

    _toggleCloseOnClickOutside(): void {
        this._closeOnClickOutside = !this._closeOnClickOutside;
    }

    _toggleCloseOnClickBackdrop(): void {
        this._closeOnClickBackdrop = !this._closeOnClickBackdrop;
    }

    _toggleShowBackdrop(): void {
        this._showBackdrop = !this._showBackdrop;
    }

    _toggleAnimate(): void {
        this._animate = !this._animate;
    }

    _toggleTrapFocus(): void {
        this._trapFocus = !this._trapFocus;
    }

    _toggleAutoFocus(): void {
        this._autoFocus = !this._autoFocus;
    }

    _toggleKeyClose(): void {
        this._keyClose = !this._keyClose;
    }

    _onOpenStart(): void {
        console.log('Sidebar opening');
    }

    _onOpened(): void {
        console.log('Sidebar opened');
    }

    _onCloseStart(): void {
        console.log('Sidebar closing');
    }

    _onClosed(): void {
        console.log('Sidebar closed');
    }

    _onTransitionEnd(): void {
        console.log('Transition ended');
    }
    ngOnInit() {
        this.breadcrumbs$ = this.router.events
            .filter(event => event instanceof NavigationEnd)
            .distinctUntilChanged()
            .map((event: any) => this.buildBreadCrumb(this.activatedRoute.root));

        this.breadcrumbs$.subscribe(
            (data: any) => {
                console.log(data);
            }
        );
    }

    private buildBreadCrumb(
        route: ActivatedRoute,
        url: string = '',
        breadcrumbs: Array<BreadCrumb> = []
    ): Array<BreadCrumb> {

        // If no routeConfig is avalailable we are on the root path
        const breadcrumbData = route.routeConfig && route.routeConfig.data && route.routeConfig.data['breadcrumb'];
        let newBreadcrumbs: any = [...breadcrumbs];
        let nextUrl = '';

        const label = route.routeConfig ? breadcrumbData : null;
        const path = route.routeConfig ? route.routeConfig.path : '';
        // In the routeConfig the complete path is not available,
        // so we rebuild it each time
        nextUrl = `${url}${path}/`;
        const breadcrumb = {
            label: label,
            url: nextUrl
        };
        if (breadcrumbData) {
            newBreadcrumbs = [...breadcrumbs, breadcrumb];
        }
        // const newBreadcrumbs = [...breadcrumbs, breadcrumb];
        if (route.firstChild) {
            // If we are not on our current path yet,
            // there will be more children to look after, to build our breadcumb
            return this.buildBreadCrumb(route.firstChild, nextUrl, newBreadcrumbs);
        }
        return newBreadcrumbs;
    }
}

export interface BreadCrumb {
    label: string;
    url: string;
}
