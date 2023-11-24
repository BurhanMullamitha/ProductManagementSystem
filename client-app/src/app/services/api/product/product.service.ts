import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../../../models/product';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productsSubject: BehaviorSubject<Product[]> = new BehaviorSubject<Product[]>([]);
  products$ = this.productsSubject.asObservable();
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {
    this.fetchAllProducts();
  }

  fetchAllProducts(): void {
    this.http.get<Product[]>(`${this.baseUrl}/api/products`).subscribe(products => {
      this.productsSubject.next(products);
    });
  }

  getAllProducts(): Observable<Product[]> {
    if (this.productsSubject.value.length > 0) {
      return of(this.productsSubject.value);
    } else {
      return this.http.get<Product[]>(`${this.baseUrl}/api/products`).pipe(
        tap((products: Product[]) => {
          this.productsSubject.next(products);
        }),
        catchError(error => {
          console.error('There was an error while fetching the products', error);
          return of([]);
        })
      );
    }
  }

  getProduct(id: string): Observable<Product> {
    const product = this.productsSubject.value.find(product => product.id === id);
    if (product) {
      return of(product);
    } else {
      return this.http.get<Product>(`${this.baseUrl}/api/products/${id}`).pipe(
        tap((product: Product) => {
          const currentProducts = this.productsSubject.value;
          currentProducts.push(product);
          this.productsSubject.next(currentProducts);
        }),
        catchError(error => {
          console.error('There was an error while fetching the product', error);
          return of({} as Product);
        })
      );
    }
  }

  createProduct(product: Product) {
    return this.http.post<Product>(`${this.baseUrl}/api/products`, product).pipe(
      tap((newProduct: Product) => {
        const currentProducts = this.productsSubject.value;
        currentProducts.push(newProduct);
        this.productsSubject.next(currentProducts);
      }),
      catchError(error => {
        console.error('There was an error while adding the product', error);
        return of({} as Product);
      })
    );
  }

  updateProduct(product: Product) {
    return this.http.put<Product>(`${this.baseUrl}/api/products/${product.id}`, product).pipe(
      tap((updatedProduct: Product) => {
        const currentProducts = this.productsSubject.value;
        const index = currentProducts.findIndex(p => p.id === updatedProduct.id);
        currentProducts[index] = updatedProduct;
        this.productsSubject.next(currentProducts);
      }),
      catchError(error => {
        console.error('There was an error while updating the product', error);
        return of({} as Product);
      })
    );
  }

  deleteProduct(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/api/products/${id}`).pipe(
      tap(() => {
        let currentProducts = this.productsSubject.value;
        currentProducts = currentProducts.filter(product => product.id !== id);
        this.productsSubject.next(currentProducts);
      }),
      catchError(error => {
        console.error('There was an error while deleting the product', error);
        return of(null);
      })
    );
  }
}
