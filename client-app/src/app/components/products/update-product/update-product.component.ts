import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { ProductService } from '../../../services/api/product/product.service';
import { CategoryService } from '../../../services/api/category/category.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormService } from '../../../services/form/product/form.service';

@Component({
  selector: 'app-update-product',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.css'
})
export class UpdateProductComponent implements OnInit {
  productForm: FormGroup = this.formService.initProductForm();
  categories: any[] = [];

  constructor(private activatedRoute: ActivatedRoute, private categoryService: CategoryService,
    private productService: ProductService, private router: Router, private formService: FormService) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.productService.getProduct(params['id']).subscribe(product => {
        this.productForm.patchValue(product);
      });
    });

    this.categoryService.getCategoriesArray().subscribe(categories => {
      this.categories = categories;
    });

    this.productForm.get('name')?.valueChanges.subscribe(() => {
      this.productForm.get('description')?.updateValueAndValidity();
    });
  }
  
  onSubmit(): void {
    if (this.productForm.valid) {
      this.productService.updateProduct(this.productForm.value).subscribe({
        next: () => {
          this.router.navigate(['/products']);
        },
        error: (error) => {
          console.error('There was an error while updating the product', error);
        }
      });
    }
  }
}
