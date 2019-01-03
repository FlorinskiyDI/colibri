import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

/* module */ import { SharedModule } from 'shared/shared.module';
/* module */ import { ReportRoutingModule } from './report.routes';
/* component */ import { ReportComponent } from './report.component';
/* component */ import { TableReportComponent } from './tableReport/tableReport.component';


@NgModule({
    imports: [
        ReportRoutingModule,
        SharedModule,
    ],
    declarations: [
        ReportComponent,
        TableReportComponent
    ],
    providers: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ReportModule { }
