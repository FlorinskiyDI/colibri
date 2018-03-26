import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './app.component';

@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent],
    imports: [
        ServerModule,
        NoopAnimationsModule,
        AppModuleShared
    ]
})

export class AppModule { }
