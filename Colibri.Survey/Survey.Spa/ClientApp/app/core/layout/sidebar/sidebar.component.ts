import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { MenuItem } from 'primeng/api';
import { MatMenuTrigger } from '@angular/material/menu';
import { Router } from '@angular/router';
import { Sidebar } from 'ng-sidebar';

@Component({
    selector: 'cmp-sidebar',
    templateUrl: 'sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {
    @ViewChild('sidebarLeft') sidebarLeft: Sidebar;
    @Input() eventSidebarToggle: Observable<any>;
    private subscriberSidebarToggle: any;
    items: MenuItem[];


    private _opened = false;
    private _modeNum = 0;
    private _positionNum = 0;
    private _dock = true;
    private _closeOnClickOutside = true;
    private _closeOnClickBackdrop = false;
    private _showBackdrop = true;
    private _animate = true;
    private _trapFocus = true;
    private _autoFocus = true;
    private _keyClose = false;
    private _autoCollapseHeight: number | null;
    private _autoCollapseWidth: number | null;
    private _MODES: Array<string> = ['over', 'push', 'slide'];
    private _POSITIONS: Array<string> = ['left', 'right', 'top', 'bottom'];

    constructor(
        public router: Router
    ) { }

    ngOnInit() {
        this.subscriberSidebarToggle = this.eventSidebarToggle.subscribe(() => this._toggleOpened());
    }
    mouseEnter(data: any, ...args: any[]) {
        data.openMenu();
        args.forEach((item: any) => { item.closeMenu(); });
    }
    ngOnDestroy() {
        this.subscriberSidebarToggle.unsubscribe();
    }
    _toggleOpened(): void { this._opened = !this._opened; }
    _toggleAutoCollapseHeight(): void { this._autoCollapseHeight = this._autoCollapseHeight ? null : 500; }
    _toggleAutoCollapseWidth(): void { this._autoCollapseWidth = this._autoCollapseWidth ? null : 500; }
    _toggleDock(): void { this._dock = !this._dock; }
    _toggleCloseOnClickOutside(): void { this._closeOnClickOutside = !this._closeOnClickOutside; }
    _toggleCloseOnClickBackdrop(): void { this._closeOnClickBackdrop = !this._closeOnClickBackdrop; }
    _toggleShowBackdrop(): void { this._showBackdrop = !this._showBackdrop; }
    _toggleAnimate(): void { this._animate = !this._animate; }
    _toggleTrapFocus(): void { this._trapFocus = !this._trapFocus; }
    _toggleAutoFocus(): void { this._autoFocus = !this._autoFocus; }
    _toggleKeyClose(): void { this._keyClose = !this._keyClose; }
    _onOpenStart(): void { /*console.log('Sidebar opening');*/ }
    _onOpened(): void { /*console.log('Sidebar opened');*/ }
    _onCloseStart(...args: MatMenuTrigger[]): void {
        args.forEach((item: MatMenuTrigger) => { if (item.menuOpen) { item.closeMenu(); } });
    }
    _onClosed(): void { }
    _onTransitionEnd(): void { /*console.log('Transition ended');*/ }
    _togglePosition(): void {
        this._positionNum++;
        if (this._positionNum === this._POSITIONS.length) { this._positionNum = 0; }
    }
    _toggleMode(): void {
        this._modeNum++;
        if (this._modeNum === this._MODES.length) { this._modeNum = 0; }
    }

    closeSidebar() {
        this.sidebarLeft.close();
    }
}
