import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { generateObjectId } from '../../../../utils/BsonFactory';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  constructor(private formBuilder: FormBuilder) { }

  initCategoryForm(): FormGroup {
    return this.formBuilder.group({
      id: [generateObjectId(), Validators.required],
      name: ['', { validators: [Validators.required], updateOn: 'change' }],
      description: ['', { validators: [Validators.required], updateOn: 'change' }]
    }, { updateOn: 'submit' })
  }
}
