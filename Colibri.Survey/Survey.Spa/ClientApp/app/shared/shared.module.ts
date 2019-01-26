import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpModule, JsonpModule } from '@angular/http';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { NgxSmoothDnDModule } from 'ngx-smooth-dnd';


/* translate */ import { TranslateModule, TranslateStore, TranslateLoader } from '@ngx-translate/core';
/* translate */ import { TranslateHttpLoader } from '@ngx-translate/http-loader';

// modules
/* module-angular-split */ import { AngularSplitModule } from 'angular-split';
/* module-shared-primeng */ import { SharedPrimeNgModule } from './shared-primeng.module';
/* module-shared-primeng */ import { SharedMaterialModule } from './shared-material.module';
/* module-ngx-bootstrap */ import { TabsModule } from 'ngx-bootstrap/tabs';
/* module-ngx-bootstrap */ import { BsDropdownModule } from 'ngx-bootstrap/dropdown';

// services
/* service-api */ import { SurveysApiService } from 'shared/services/api/surveys.api.service';
/* service-api */ import { PortalApiService } from 'shared/services/api/portal.api.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* service-api */ import { GroupMembersApiService } from 'shared/services/api/group-members.api.service';
/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';

// directives
/* directive */ import { HasPermissionDirective } from 'shared/directives/has-permission/has-permission.directive';

// components
/* component */ import { DataFilterComponent } from 'shared/directives/data-filter/data-filter.component';
/* component-modal */ import { ModalComponent } from 'shared/directives/modal/modal.component';
/* component-modal */ import { ModalService } from 'shared/directives/modal/modal.service';

import { FragmentPolyfillModule } from './helpers/fragment-polyfill.module';

@NgModule({
    imports: [
        // modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        JsonpModule,
        HttpModule,
        HttpClientModule,
        NgxSmoothDnDModule,
        SharedPrimeNgModule,
        SharedMaterialModule,
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (http: HttpClient) => new TranslateHttpLoader(http),
                deps: [HttpClient]
            }
        }),
        FragmentPolyfillModule.forRoot({
            smooth: true
        }),
        // ngx-bootstrap
        TabsModule.forRoot(), BsDropdownModule.forRoot(),
        // material

        // else
        AngularSplitModule
    ],
    declarations: [
        // diractives
        HasPermissionDirective,
        ModalComponent,
        DataFilterComponent
    ],
    providers: [
        ModalService
    ],
    exports: [
        // modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        FragmentPolyfillModule,
        TranslateModule,
        SharedPrimeNgModule,
        SharedMaterialModule,
        // MdlModule,
        // ngx-bootstrap
        TabsModule, BsDropdownModule,
        // else
        AngularSplitModule,
        // directives
        HasPermissionDirective,
        ModalComponent,
        DataFilterComponent
    ]
})

export class SharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                TranslateStore,
                // api service
                GroupsApiService,
                GroupMembersApiService,
                SurveysApiService,
                UsersApiService,
                PortalApiService,
                // diractives
                HasPermissionDirective
                // ModalService
            ]
        };
    }
}
