import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../../models/product';
import { ProductService } from '../../../services/api/product/product.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './product-list.component.html'
})

export class ProductListComponent implements OnInit {

  products: Product[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe({
      next: (products: Product[]) => {
        this.products = products;
      },
      error: (error: any) => {
        console.error('There was an error while fetching the products', error);
      }
    });
  }

  deleteProduct(id: string): void {
    this.productService.deleteProduct(id)
    .subscribe({
      next: () => {
      },
      error: (errResponse) => {
        console.log(errResponse);
      }
    })
  }
}
