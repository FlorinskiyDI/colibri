import { Directive, ElementRef, Inject, Input, NgZone, Optional, ViewContainerRef, AfterViewInit } from '@angular/core';
import { MAT_TOOLTIP_DEFAULT_OPTIONS, MAT_TOOLTIP_SCROLL_STRATEGY, MatTooltip, MatTooltipDefaultOptions } from '@angular/material';
import { AriaDescriber, FocusMonitor } from '@angular/cdk/a11y';
import { Directionality } from '@angular/cdk/bidi';
import { Overlay, ScrollDispatcher } from '@angular/cdk/overlay';
import { Platform } from '@angular/cdk/platform';
/**/
/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';

@Directive({
    selector: '[hasPermission]',
    exportAs: 'hasPermission'
})

export class HasPermissionDirective extends MatTooltip implements AfterViewInit {
    private currentUser: any;
    private _permissions: any[] = [];
    private _isDisable = false;
    private _isHidden = false;
    private _permissionCtrl: any;
    private _logicalOp = 'AND';
    private HAS_PERMISSION_CTRL_ATTRIBUTE = 'hasPermissionCtrl';

    @Input() set hasPermission(value: any) { this._permissions = value; }
    @Input() set IsHidden(value: boolean) { this._isHidden = value; }
    @Input() set IsDisable(value: boolean) { this._isDisable = value; }

    constructor(
        private securityService: OidcSecurityService,
        private element: ElementRef,
        // params for super MatTooltip
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
            _overlay, _elementRef, _scrollDispatcher, _viewContainerRef, _ngZone,
            _platform, _ariaDescriber, _focusMonitor, _scrollStrategy, _dir, _defaultOptions
        );
    }

    ngOnInit() { }
    ngAfterViewInit() {
        this.securityService.getUserData().subscribe((user: any) => {
            console.log(user);
            this.currentUser = user;
            this.updateView();
        });
    }

    private updateView() {
        const result = this.checkPermission();
        if (!result) {
            if (this._isHidden) {
                // hide element
                this.element.nativeElement.hidden = true;
            } else {
                // set  message of overrrided tooltip
                this.message = `
                You need permissions for this action.
                Required permission(s): ${this._permissions.join(', ')}
                `;
                // disable children elements with attribute
                const elementWithAtribute: any = this._getElementByAttribute(this.HAS_PERMISSION_CTRL_ATTRIBUTE, this.element.nativeElement);
                if (this._isDisable) {
                    elementWithAtribute.disabled = this._isDisable;
                }
            }
        }
    }

    private checkPermission() {
        let hasPermission = false;
        if (this.currentUser && this.currentUser.permission) {
            for (const checkPermission of this._permissions) {
                const permissionFound = this.currentUser.permission.find((x: any) => x.toUpperCase() === checkPermission.toUpperCase());
                if (permissionFound) {
                    hasPermission = true;
                    if (this._logicalOp === 'OR') { break; }
                } else {
                    hasPermission = false;
                    if (this._logicalOp === 'AND') { break; }
                }
            }
        }
        return hasPermission;
    }

    private _getElementByAttribute(attr: any, root: any) {
        if (root.hasAttribute(attr)) { return root; }
        const children: any = this._permissionCtrl = root.children;

        for (let i = children.length; i--;) {
            this._permissionCtrl = this._getElementByAttribute(attr, children[i]);
            if (this._permissionCtrl) {
                return this._permissionCtrl;
            }
        }
        return null;
    }
}
