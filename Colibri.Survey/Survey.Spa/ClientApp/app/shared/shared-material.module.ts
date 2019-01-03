import { NgModule, ModuleWithProviders } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MatIconRegistry } from '@angular/material';

/* module-material */ import { MatIconModule } from '@angular/material/icon';
/* module-material */ import { MatTooltipModule } from '@angular/material/tooltip';
/* module-material */ import { MatButtonModule } from '@angular/material/button';
/* module-material */ import { MatInputModule } from '@angular/material/input';
/* module-material */ import { MatSelectModule } from '@angular/material/select';
/* module-material */ import { MatChipsModule } from '@angular/material/chips';
/* module-material */ import { MatCheckboxModule } from '@angular/material/checkbox';
/* module-material */ import { MatAutocompleteModule } from '@angular/material/autocomplete';

@NgModule({
    imports: [
        MatTooltipModule,
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatChipsModule,
        MatCheckboxModule,
        MatAutocompleteModule,
        MatIconModule,
    ],
    declarations: [ ],
    providers: [ ],
    exports: [
        MatTooltipModule,
        MatButtonModule,
        MatInputModule,
        MatSelectModule,
        MatChipsModule,
        MatCheckboxModule,
        MatAutocompleteModule,
        MatIconModule,
    ]
})

export class SharedMaterialModule {
    public static forRoot(): ModuleWithProviders {
        return {
            ngModule: SharedMaterialModule,
            providers: [ ]
        };
    }

    constructor(
        iconRegistry: MatIconRegistry,
        sanitizer: DomSanitizer
    ) {
        iconRegistry.addSvgIcon('twotone-question_answer', sanitizer.bypassSecurityTrustResourceUrl('assets/images/icons-material/twotone-question_answer-24px.svg'));
        iconRegistry.addSvgIcon('azure-api-management', sanitizer.bypassSecurityTrustResourceUrl('assets/images/icons-material/azure-api-management.svg'));
        iconRegistry.addSvgIcon('build-queue-new', sanitizer.bypassSecurityTrustResourceUrl('assets/images/icons-material/build-queue-new.svg'));
        iconRegistry.addSvgIcon('work-item', sanitizer.bypassSecurityTrustResourceUrl('assets/images/icons-material/work-item.svg'));
    }
}
