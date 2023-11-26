import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import { Product } from '../../../models/product';
import { environment } from '../../../../environments/environment.development';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });

    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get all products', () => {
    const dummyProducts: Product[] = [
      { id: '1', name: 'Product 1', description: 'Product 1 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 100, color: 'red' },
      { id: '2', name: 'Product 2', description: 'Product 2 Description', categoryId: '2', categoryName: 'Laptops', price: 200, color: 'blue' }
    ];

    service.getAllProducts().subscribe(products => {
      expect(products.length).toBe(2);
      expect(products).toEqual(dummyProducts);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProducts);
  });

  it('should handle error for getAllProducts', () => {
    service.getAllProducts().subscribe({
      next: products => fail('should have failed with a Network error'),
      error: error => expect(error).toEqual(new Error('There was an error while fetching the products'))
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products`);
    expect(req.request.method).toBe('GET');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should get a product by id', () => {
    const dummyProduct: Product = { id: '1', name: 'Product 1', description: 'Product 1 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 100, color: 'red' };

    service.getProduct(dummyProduct.id).subscribe(product => {
      expect(product).toEqual(dummyProduct);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/${dummyProduct.id}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProduct);
  });

  it('should handle error for getProduct', () => {
    service.getProduct('1').subscribe({
      next: product => fail('should have failed with a Network error'),
      error: error => expect(error).toEqual(new Error('There was an error while fetching the product'))
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/1`);
    expect(req.request.method).toBe('GET');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should create a product', () => {
    const dummyProduct: Product = { id: '3', name: 'Product 3', description: 'Product 3 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 100, color: 'red' };

    service.createProduct(dummyProduct).subscribe(product => {
      expect(product).toEqual(dummyProduct);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products`);
    expect(req.request.method).toBe('POST');
    req.flush(dummyProduct);
  });

  it('should handle error for createProduct', () => {
    const dummyProduct: Product = { id: '3', name: 'Product 3', description: 'Product 3 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 100, color: 'red' };

    service.createProduct(dummyProduct).subscribe({
      next: product => fail('should have failed with a Network error'),
      error: error => expect(error).toEqual(new Error('There was an error while adding the product'))
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products`);
    expect(req.request.method).toBe('POST');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should update a product', () => {
    const dummyProduct: Product = { id: '1', name: 'Product 1', description: 'Product 1 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 150, color: 'red' };

    service.updateProduct(dummyProduct).subscribe(product => {
      expect(product).toEqual(dummyProduct);
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(dummyProduct);
  });

  it('should handle error for updateProduct', () => {
    const dummyProduct: Product = { id: '1', name: 'Product 1', description: 'Product 1 Description', categoryId: '1', categoryName: 'Mobile Phones', price: 150, color: 'red' };
  
    service.updateProduct(dummyProduct).subscribe({
      next: product => fail('should have failed with a Network error'),
      error: error => expect(error).toEqual(new Error('There was an error while updating the product'))
    });
  
    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/${dummyProduct.id}`);
    expect(req.request.method).toBe('PUT');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should delete a product', () => {
    service.deleteProduct('1').subscribe(res => {
      expect(res).toBeNull();
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });
  
  it('should handle error for deleteProduct', () => {
    service.deleteProduct('1').subscribe({
      next: res => fail('should have failed with a Network error'),
      error: error => expect(error).toEqual(new Error('There was an error while deleting the product'))
    });

    const req = httpMock.expectOne(`${environment.baseUrl}/api/products/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });
});
