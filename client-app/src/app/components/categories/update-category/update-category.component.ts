import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../../services/api/category/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../services/form/category/form.service';

@Component({
  selector: 'app-update-category',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './update-category.component.html'
})

export class UpdateCategoryComponent implements OnInit {
  categoryForm: FormGroup = this.formService.initCategoryForm();

  constructor(private categoryService: CategoryService, 
    private activatedRoute: ActivatedRoute, private router: Router, private formService: FormService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.categoryService.getCategory(params['id']).subscribe(category => {
        this.categoryForm.patchValue(category);
      });
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      this.categoryService.updateCategory(this.categoryForm.value).subscribe({
        next: () => {
          this.router.navigate(['/categories']);
        },
        error: (error) => {
          console.error('There was an error while updating the category', error);
        }
      });
    }
  }
}