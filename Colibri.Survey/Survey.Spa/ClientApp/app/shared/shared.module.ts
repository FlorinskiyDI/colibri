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

/* primeng */ import { RadioButtonModule } from 'primeng/components/radiobutton/radiobutton';
/* primeng */ import { ListboxModule } from 'primeng/components/listbox/listbox';
/* primeng */ import { ChartModule } from 'primeng/components/chart/chart';
/* primeng */ import { TooltipModule } from 'primeng/components/tooltip/tooltip';
/* primeng */ import { ColorPickerModule } from 'primeng/components/colorpicker/colorpicker';
/* primeng */ import { ScheduleModule } from 'primeng/components/schedule/schedule';
/* primeng */ import { ButtonModule } from 'primeng/components/button/button';
/* primeng */ import { OverlayPanelModule } from 'primeng/components/overlaypanel/overlaypanel';
/* primeng */ import { InputTextModule } from 'primeng/components/inputtext/inputtext';
/* primeng */ import { AutoCompleteModule } from 'primeng/components/autocomplete/autocomplete';
/* primeng */ import { DropdownModule } from 'primeng/components/dropdown/dropdown';
/* primeng */ import { GrowlModule } from 'primeng/components/growl/growl';
/* primeng */ import { DialogModule } from 'primeng/components/dialog/dialog';
/* primeng */ import { DataTableModule } from 'primeng/components/datatable/datatable';
/* primeng */ import { FileUploadModule } from 'primeng/components/fileupload/fileupload';
/* primeng */ import { CalendarModule } from 'primeng/components/calendar/calendar';
/* primeng */ import { CheckboxModule } from 'primeng/components/checkbox/checkbox';
/* primeng */ import { InputSwitchModule } from 'primeng/components/inputswitch/inputswitch';
/* primeng */ import { InputTextareaModule } from 'primeng/components/inputtextarea/inputtextarea';
/* primeng */ import { TabMenuModule } from 'primeng/components/tabmenu/tabmenu';
/* primeng */ import { TabViewModule } from 'primeng/components/tabview/tabview';
/* primeng */ import { AccordionModule } from 'primeng/components/accordion/accordion';
/* primeng */ import { ConfirmDialogModule } from 'primeng/components/confirmdialog/confirmdialog';

/* material */ import { MatTooltipModule } from '@angular/material/tooltip';
/* material */import { MatIconModule } from '@angular/material/icon';

/* ngx-bootstrap */ import { TabsModule } from 'ngx-bootstrap/tabs';
/* ngx-bootstrap */ import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
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
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule,
        // ngx-bootstrap
        TabsModule.forRoot(), BsDropdownModule.forRoot(),
        // material
        MatTooltipModule, MatIconModule
    ],
    declarations: [],
    entryComponents: [],
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
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule,
        // ngx-bootstrap
        TabsModule, BsDropdownModule,
        // material
        MatTooltipModule, MatIconModule
    ]
})

export class SharedModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedModule,
            providers: [
                TranslateStore
            ]
        };
    }
}
