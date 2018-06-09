import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

import { SharedModule } from './shared/shared.module';
/* module */ import { CoreModule } from 'core/core.module';
/* shared */ import { CoreModuleShared } from 'core//core.module.shared';
/* route */ import { routing } from './app.routes';
/* guard */ import { HasAdminRoleAuthenticationGuard } from './guards/hasAdminRoleAuthenticationGuard';
/* guard */ import { HasAdminRoleCanLoadGuard } from './guards/hasAdminRoleCanLoadGuard';
import { QuestionTransferService } from './shared/transfers/question-transfer.service';
import { DndModule } from 'ng2-dnd';

@NgModule({
    imports: [
        routing,
        CoreModule,
        CoreModuleShared,
        DndModule.forRoot(),
        SharedModule.forRoot()
    ],
    declarations: [],
    providers: [
        HasAdminRoleAuthenticationGuard,
        HasAdminRoleCanLoadGuard,
        QuestionTransferService
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        DndModule,
        SharedModule
    ]
})

export class AppModuleShared { }

