import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../../services/api/category/category.service';
import { Router } from '@angular/router';
import { FormService } from '../../../services/form/category/form.service';

@Component({
  selector: 'app-add-category',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-category.component.html'
})

export class AddCategoryComponent {
  categoryForm: FormGroup = this.formService.initCategoryForm();

  constructor(private categoryService: CategoryService, 
    private router: Router, private formService: FormService) { }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      this.categoryService.createCategory(this.categoryForm.value).subscribe({
        next: () => {
          this.router.navigate(['/categories']);
        },
        error: (error) => {
          console.error('There was an error while adding the category', error);
        }
      });
    }
  }
}
