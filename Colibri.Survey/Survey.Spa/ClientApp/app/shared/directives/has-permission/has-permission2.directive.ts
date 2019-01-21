import { Directive, ElementRef, Inject, Input, TemplateRef, NgZone, Optional, ViewContainerRef } from '@angular/core';
import {
    MAT_TOOLTIP_DEFAULT_OPTIONS,
    MAT_TOOLTIP_SCROLL_STRATEGY,
    MatTooltip,
    MatTooltipDefaultOptions
} from '@angular/material';
import { AriaDescriber, FocusMonitor } from '../../../../../node_modules/@angular/cdk/a11y';
import { Directionality } from '../../../../../node_modules/@angular/cdk/bidi';
import { Overlay, ScrollDispatcher } from '../../../../../node_modules/@angular/cdk/overlay';
import { Platform } from '../../../../../node_modules/@angular/cdk/platform';

@Directive({
    selector: '[mytooltip]',
    exportAs: 'mytooltip'
})
export class HasPermissionDirective2 extends MatTooltip {
    private currentUser: any;
    private permissions: any[] = [];
    private logicalOp = 'AND';
    private isHidden = true;
    private test: any;
    @Input()
    get mytooltip() {
        return this.message;
    }
    set mytooltip(value: any) {
        debugger
        const test = this.element;
        if (!this.checkPermission()) {
            debugger
            this.message = `
            You need permissions for this action.
            Required permission(s): ${value}
        `;
            // debugger
            // this.element.nativeElement.disabled = true;
            // this.test.createEmbeddedView(this.templateRef);
        }
    }

    constructor(
        // private viewContainer: ViewContainerRef,
        // private templateRef: TemplateRef<any>,
        private element: ElementRef,
        // MatTooltip params
        _overlay: Overlay,
        _elementRef: ElementRef,
        _scrollDispatcher: ScrollDispatcher,
        _viewContainerRef: ViewContainerRef,
        _ngZone: NgZone,
        _platform: Platform,
        _ariaDescriber: AriaDescriber,
        _focusMonitor: FocusMonitor,
        @Inject(MAT_TOOLTIP_SCROLL_STRATEGY) _scrollStrategy: any,
        @Optional() _dir: Directionality,
        @Optional() @Inject(MAT_TOOLTIP_DEFAULT_OPTIONS)
        _defaultOptions: MatTooltipDefaultOptions
    ) {
        super(
            _overlay,
            _elementRef,
            _scrollDispatcher,
            _viewContainerRef,
            _ngZone,
            _platform,
            _ariaDescriber,
            _focusMonitor,
            _scrollStrategy,
            _dir,
            _defaultOptions
        );
        // this.test = _viewContainerRef;
    }





    private checkPermission() {
        let hasPermission = false;
        if (this.currentUser && this.currentUser.permission) {
            for (const checkPermission of this.permissions) {
                const permissionFound = this.currentUser.permission.find((x: any) => x.toUpperCase() === checkPermission.toUpperCase());

                if (permissionFound) {
                    hasPermission = true;

                    if (this.logicalOp === 'OR') {
                        break;
                    }
                } else {
                    hasPermission = false;
                    if (this.logicalOp === 'AND') {
                        break;
                    }
                }
            }
        }

        return hasPermission;
    }
}
