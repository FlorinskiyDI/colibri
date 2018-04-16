import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

/* module */ import { CoreModule } from 'core/core.module';
/* shared */ import { CoreModuleShared } from 'core//core.module.shared';
/* route */ import { routing } from './app.routes';
/* guard */ import { HasAdminRoleAuthenticationGuard } from './guards/hasAdminRoleAuthenticationGuard';
/* guard */ import { HasAdminRoleCanLoadGuard } from './guards/hasAdminRoleCanLoadGuard';
/* service */ import { UserManagementService } from './user-management/UserManagementService';
import { DndModule } from 'ng2-dnd';

@NgModule({
    imports: [
        routing,
        CoreModule,
        CoreModuleShared,
        DndModule.forRoot(),
    ],
    declarations: [ ],
    providers: [
        UserManagementService,
        HasAdminRoleAuthenticationGuard,
        HasAdminRoleCanLoadGuard
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        DndModule
    ]
})

export class AppModuleShared { }

