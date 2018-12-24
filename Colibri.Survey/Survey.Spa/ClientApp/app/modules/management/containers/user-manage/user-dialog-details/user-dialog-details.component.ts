import { Component, Output, Input, EventEmitter } from '@angular/core';

/* model-control */ import { DialogDataModel } from 'shared/models/controls/dialog-data.model';
/* service-api */ import { UsersApiService } from 'shared/services/api/users.api.service';

@Component({
    selector: 'user-dialog-details-cmp',
    templateUrl: './user-dialog-details.component.html',
    styleUrls: ['./user-dialog-details.component.scss']
})

export class UserDialogDetailsComponent {
    // dialog variable
    dialogConfig: DialogDataModel<any> = new DialogDataModel();
    @Output() onChange = new EventEmitter<any>();
    @Output() onCancel = new EventEmitter<any>();
    @Output() onHide = new EventEmitter<any>();
    @Input()
    get config() { return this.dialogConfig; }
    set config(data: DialogDataModel<any>) {
        if (data) {
            this.dialogConfig = data;
            this.componentInit(data.extraData.userId);
        }
    }
    constructor(
        private usersApiService: UsersApiService
    ) {
    }


    ngOnInit() { }
    ngOnDestroy() { this.componentClear(); }
    private componentInit(userId: any) {
        this.usersApiService.getFullInfo(userId).subscribe(
            (data: any) => {
                console.log(data);
            }
        );
    }
    private componentClear() {
    }


    //#region Dialog
    public dialogCancel() {
        this.dialogConfig.visible = false;
        this.onCancel.emit();
    }
    public dialogChange(data: any | null = null) {
        this.dialogConfig.visible = false;
        this.onChange.emit(data);
    }
    public dialogHide() {
        this.onHide.emit();
        this.componentClear();
    }
    //#endregion end dialog

}
