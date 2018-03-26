import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppModuleShared } from './app.module.shared';
import { AppComponent } from './app.component';
declare let window: any;

@NgModule({
    bootstrap: [AppComponent],
    declarations: [AppComponent],
    imports: [
        AppModuleShared,
        BrowserAnimationsModule
    ],
    providers: [
        { provide: 'API_URL', useValue: getApiUrl() }
    ]
})
export class AppModule {
}

export function getApiUrl() {
    return window['serverSettings']!.AlarmApiUrl;
}

