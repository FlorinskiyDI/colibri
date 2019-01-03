import { NgModule, ModuleWithProviders } from '@angular/core';

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
/* module-primeng */ import { MultiSelectModule } from 'primeng/multiselect';
/* module-primeng */ import { SplitButtonModule } from 'primeng/splitbutton';

@NgModule({
    imports: [
        TabViewModule, TabMenuModule, ConfirmDialogModule, AccordionModule, InputTextareaModule, InputSwitchModule, CheckboxModule, CalendarModule,
        FileUploadModule, DataTableModule, DialogModule, GrowlModule, DropdownModule, AutoCompleteModule, InputTextModule, OverlayPanelModule, ButtonModule,
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule, TableModule, FieldsetModule,
        TreeTableModule, TreeModule, BlockUIModule, PanelModule, DragDropModule, ChipsModule, MultiSelectModule, SplitButtonModule,
    ],
    declarations: [ ],
    providers: [ ],
    exports: [
        TabViewModule, TabMenuModule, ConfirmDialogModule, AccordionModule, InputTextareaModule, InputSwitchModule, CheckboxModule, CalendarModule,
        FileUploadModule, DataTableModule, DialogModule, GrowlModule, DropdownModule, AutoCompleteModule, InputTextModule, OverlayPanelModule, ButtonModule,
        ScheduleModule, TooltipModule, ChartModule, ColorPickerModule, TooltipModule, ListboxModule, RadioButtonModule, TableModule, FieldsetModule,
        TreeTableModule, TreeModule, BlockUIModule, PanelModule, DragDropModule, ChipsModule, MultiSelectModule, SplitButtonModule,

    ]
})

export class SharedPrimeNgModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedPrimeNgModule,
            providers: [ ]
        };
    }
}
