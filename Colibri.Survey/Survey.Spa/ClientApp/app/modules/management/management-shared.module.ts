import { NgModule, ModuleWithProviders } from '@angular/core';

/* pipe */ import { UserStatusPipe } from './common/pipes/user-status.pipe';
/* service */ import { UserService } from './common/services/user.service';

@NgModule({
    imports: [
    ],
    declarations: [
        // pipes
        UserStatusPipe
    ],
    providers: [
        UserService
    ],
    exports: [
        // pipes
        UserStatusPipe
    ]
})

export class ManagementSharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: ManagementSharedModule,
            providers: [
                UserService
            ]
        };
    }
}
