import { Directive, Input, TemplateRef, ViewContainerRef, ElementRef, OnInit, HostBinding } from '@angular/core';
import { MatTooltip } from '@angular/material/tooltip';

/* service */ import { OidcSecurityService } from 'core/auth/services/oidc.security.service';

@Directive({
    selector: '[hasPermission]'
})
export class HasPermissionDirective implements OnInit {
    private currentUser: any;
    private permissions: any[] = [];
    private logicalOp = 'AND';
    private isHidden = true;

    matTooltipDirective: MatTooltip;

    constructor(
        private element: ElementRef,
        private templateRef: TemplateRef<any>,
        private viewContainer: ViewContainerRef,
        private securityService: OidcSecurityService
    ) {
    }

    ngOnInit() {
        this.securityService.getUserData().subscribe((user: any) => {
            this.currentUser = user;

            this.updateView();
        });
    }

    @Input()
    set hasPermission(val: any[]) {
        this.permissions = val;
        this.updateView();
    }

    @Input()
    set hasPermissionOperation(permop: string) {
        this.logicalOp = permop;
        this.updateView();
    }

    private updateView() {
        if (this.checkPermission()) {
            if (this.isHidden) {
                this.viewContainer.createEmbeddedView(this.templateRef);
                this.isHidden = false;
            }
        } else {
            this.isHidden = true;
            this.viewContainer.clear();
        }
    }

    private checkPermission() {
        let hasPermission = false;
        debugger
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
