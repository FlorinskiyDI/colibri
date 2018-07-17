import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { ReportRoutingModule } from './report.routes';
/* component */ import { ReportComponent } from './report.component';
/* component */ import { TableReportComponent } from './tableReport/tableReport.component';


import {CellGridComponent} from './tableReport/cellGridQuestion/cellGrid.component';
// import { DndModule } from 'ng2-dnd';
@NgModule({
    imports: [
        ReportRoutingModule,
        SharedModule,
        // DndModule.forRoot(),
    ],
    declarations: [
        ReportComponent,
        TableReportComponent,
        CellGridComponent
    ],
    // entryComponents: [
    //     FormBuilderComponent,
    //   ],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ReportModule { }
