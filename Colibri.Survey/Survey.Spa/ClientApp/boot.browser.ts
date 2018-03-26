import './polyfills';
import 'bootstrap';
import 'jquery';

import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/skip';
import 'rxjs/add/operator/mergeMap';

declare const module: any;

import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';

import { AppModule } from './app/app.module.browser';

// Enable either Hot Module Reloading or production mode
if (module.hot) {
    module.hot.accept();
    module.hot.dispose(() => {
        // Before restarting the app, we create a new root element and dispose the old one
        const oldRootElem = document.querySelector('app-root');
        const newRootElem = document.createElement('app-root');
        if (oldRootElem && oldRootElem.parentNode) { oldRootElem.parentNode.insertBefore(newRootElem, oldRootElem); }
        // modulePromise.then(appModule => appModule.destroy());
        // tslint:disable-next-line:no-use-before-declare
        modulePromise.then(appModule => {
            appModule.destroy();
            oldRootElem!.parentNode!.removeChild(oldRootElem!);
        });
    });
} else {
    enableProdMode();
}

// Note: @ng-tools/webpack looks for the following expression when performing production
// builds. Don't change how this line looks, otherwise you may break tree-shaking.
const modulePromise = platformBrowserDynamic().bootstrapModule(AppModule);
