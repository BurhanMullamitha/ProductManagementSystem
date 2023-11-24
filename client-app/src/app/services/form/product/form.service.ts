import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { generateObjectId } from '../../../../utils/BsonFactory';

@Injectable({
  providedIn: 'root'
})
export class FormService {
  constructor(private formBuilder: FormBuilder) { }

  initProductForm(): FormGroup {
    const form = this.formBuilder.group({
      id: [generateObjectId(), Validators.required],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(150)]],
      price: ['', Validators.required],
      categoryId: ['', Validators.required],
      color: ['']
    });

    form.get('description')?.setValidators([Validators.required, Validators.maxLength(150), this.containsProductNameValidator(form)]);
    return form;
  }

  containsProductNameValidator(form: FormGroup): ValidatorFn {
    return (control): {[key: string]: any} | null => {
      const name = form.get('name')?.value.toLowerCase();
      const description = control.value.toLowerCase();
      const containsProductName = name && description.includes(name);
      return containsProductName ? null : {forbiddenName: {value: control.value}};
    };
  }
}
