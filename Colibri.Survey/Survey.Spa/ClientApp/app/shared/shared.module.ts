import { NgModule, ModuleWithProviders } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpModule, JsonpModule } from '@angular/http';
import { HttpClientModule, HttpClient } from '@angular/common/http';

/* translate */ import { TranslateModule } from '@ngx-translate/core';
/* translate */ import { TranslateStore } from '@ngx-translate/core/src/translate.store';
/* translate */ import { TranslateLoader } from '@ngx-translate/core';
/* translate */ import { TranslateHttpLoader } from '@ngx-translate/http-loader';

/* module-primeng */ import { RadioButtonModule } from 'primeng/components/radiobutton/radiobutton';
/* module-primeng */ import { ListboxModule } from 'primeng/components/listbox/listbox';
/* module-primeng */ import { ChartModule } from 'primeng/components/chart/chart';
/* module-primeng */ import { TooltipModule } from 'primeng/components/tooltip/tooltip';
/* module-primeng */ import { ColorPickerModule } from 'primeng/components/colorpicker/colorpicker';
/* module-primeng */ import { ScheduleModule } from 'primeng/components/schedule/schedule';
/* module-primeng */ import { ButtonModule } from 'primeng/components/button/button';
/* module-primeng */ import { OverlayPanelModule } from 'primeng/components/overlaypanel/overlaypanel';
/* module-primeng */ import { InputTextModule } from 'primeng/components/inputtext/inputtext';
/* module-primeng */ import { AutoCompleteModule } from 'primeng/components/autocomplete/autocomplete';
/* module-primeng */ import { DropdownModule } from 'primeng/components/dropdown/dropdown';
/* module-primeng */ import { GrowlModule } from 'primeng/components/growl/growl';
/* module-primeng */ import { DialogModule } from 'primeng/components/dialog/dialog';
/* module-primeng */ import { DataTableModule } from 'primeng/components/datatable/datatable';
/* module-primeng */ import { FileUploadModule } from 'primeng/components/fileupload/fileupload';
/* module-primeng */ import { CalendarModule } from 'primeng/components/calendar/calendar';
/* module-primeng */ import { CheckboxModule } from 'primeng/components/checkbox/checkbox';
/* module-primeng */ import { InputSwitchModule } from 'primeng/components/inputswitch/inputswitch';
/* module-primeng */ import { InputTextareaModule } from 'primeng/components/inputtextarea/inputtextarea';
/* module-primeng */ import { TabMenuModule } from 'primeng/components/tabmenu/tabmenu';
/* module-primeng */ import { TabViewModule } from 'primeng/components/tabview/tabview';
/* module-primeng */ import { AccordionModule } from 'primeng/components/accordion/accordion';
/* module-primeng */ import { ConfirmDialogModule } from 'primeng/components/confirmdialog/confirmdialog';
/* module-primeng */ import { TableModule } from 'primeng/table';
/* module-primeng */ import { FieldsetModule } from 'primeng/fieldset';
/* module-primeng */ import { TreeTableModule } from 'primeng/treetable';
/* module-primeng */ import { TreeModule } from 'primeng/tree';
/* module-primeng */ import { BlockUIModule } from 'primeng/blockui';
/* module-primeng */ import { PanelModule } from 'primeng/panel';
/* module-primeng */ import { DragDropModule } from 'primeng/dragdrop';
/* module-primeng */ import { ChipsModule } from 'primeng/chips';
/* module-angular-split */ import { AngularSplitModule } from 'angular-split';


/* module-ngx-bootstrap */ import { TabsModule } from 'ngx-bootstrap/tabs';
/* module-ngx-bootstrap */ import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


// material
// /* module-material */import { MatIconModule } from '@angular/material/icon';
// /* module-material */ import { MatTooltipModule } from '@angular/material/tooltip';



/* service-api */ import { SurveysApiService } from 'shared/services/api/surveys.api.service';
/* service-api */ import { GroupsApiService } from 'shared/services/api/groups.api.service';
/* service-api */ import { GroupMembersApiService } from 'shared/services/api/group-members.api.service';
/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';

// import { FormBuilderComponent } from '../modules/surveys/builder/form-builder/form-builder.component';

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
        // Translate
        TranslateModule.forChild({
            loader: {
                provide: TranslateLoader,
                useFactory: (http: HttpClient) => new TranslateHttpLoader(http),
                deps: [HttpClient]
            }
        }),
        // primeng
        TabViewModule, TabMenuModule, ConfirmDialogModule, AccordionModule, InputTextareaModule, InputSwitchModule, CheckboxModule, CalendarModule,
        FileUploadModule, DataTableModule, DialogModule, GrowlModule, DropdownModule, AutoCompleteModule, InputTextModule, OverlayPanelModule, ButtonModule,
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule, TableModule, FieldsetModule,
        TreeTableModule, TreeModule, BlockUIModule, PanelModule, DragDropModule, ChipsModule,
        // ngx-bootstrap
        TabsModule.forRoot(), BsDropdownModule.forRoot(),
        // material
        // MatTooltipModule,
        //  MatIconModule,
        // else
        AngularSplitModule
    ],
    declarations: [
        // FormBuilderComponent,
    ],
    entryComponents: [
        // FormBuilderComponent
    ],
    exports: [
        // modules
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        TranslateModule,
        // primeng
        TabViewModule, TabMenuModule, ConfirmDialogModule, AccordionModule, InputTextareaModule, InputSwitchModule, CheckboxModule, CalendarModule,
        FileUploadModule, DataTableModule, DialogModule, GrowlModule, DropdownModule, AutoCompleteModule, InputTextModule, OverlayPanelModule, ButtonModule,
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule, TableModule, FieldsetModule,
        TreeTableModule, TreeModule, BlockUIModule, PanelModule, DragDropModule, ChipsModule,
        // ngx-bootstrap
        TabsModule, BsDropdownModule,
        // material
        // MatTooltipModule,
        // MatIconModule,
        // else
        AngularSplitModule
    ],
})

export class SharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                TranslateStore,
                GroupsApiService,
                GroupMembersApiService,
                SurveysApiService,
                UsersApiService
            ]
        };
    }
}
