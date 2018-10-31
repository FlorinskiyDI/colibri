import { FormGroup, FormControl, FormArray } from '@angular/forms';

export class FormGroupHelper {

    static setFormControlsAsTouched(group: FormGroup | FormArray) {
        group.markAsTouched();
        for (const i in group.controls) {
          if (group.get(i) instanceof FormControl) {
            group.get(i).markAsTouched();
          } else {
            this.setFormControlsAsTouched(<FormArray>group.get(i));
          }
        }
      }

}