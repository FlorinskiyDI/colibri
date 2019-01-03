import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Component({
    selector: 'cmp-sidebar',
    templateUrl: 'sidebar.component.html',
    styleUrls: ['./sidebar.component.scss']
})

export class SidebarComponent implements OnInit {
    @Input() eventSidebarToggle: Observable<any>;
    private subscriberSidebarToggle: any;

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

    constructor() {
        
    }
    ngOnInit() {
        this.subscriberSidebarToggle = this.eventSidebarToggle.subscribe(() => this._toggleOpened());
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
    _onCloseStart(): void { /*console.log('Sidebar closing');*/ }
    _onClosed(): void { /*console.log('Sidebar closed');*/ }
    _onTransitionEnd(): void { /*console.log('Transition ended');*/ }
    _togglePosition(): void {
        this._positionNum++;
        if (this._positionNum === this._POSITIONS.length) { this._positionNum = 0; }
    }
    _toggleMode(): void {
        this._modeNum++;
        if (this._modeNum === this._MODES.length) { this._modeNum = 0; }
    }
}
