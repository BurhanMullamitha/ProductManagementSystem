import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CategoryService } from '../../../services/api/category/category.service';
import { ProductService } from '../../../services/api/product/product.service';
import { FormService } from '../../../services/form/product/form.service';

import { Router } from '@angular/router';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './add-product.component.html'
})

export class AddProductComponent implements OnInit {
  productForm: FormGroup = this.formService.initProductForm();

  categories: any[] = [];

  constructor(private categoryService: CategoryService,
    private productService: ProductService, private router: Router, private formService: FormService) { }

  ngOnInit(): void {
    this.categoryService.getCategoriesArray().subscribe(categories => {
      this.categories = categories;
    });

    this.productForm.get('name')?.valueChanges.subscribe(() => {
      this.productForm.get('description')?.updateValueAndValidity();
    });
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      this.productService.createProduct(this.productForm.value).subscribe({
        next: () => {
          this.router.navigate(['/products']);
        },
        error: (error) => {
          console.error('There was an error while adding the product', error);
        }
      });
    }
  }
}
