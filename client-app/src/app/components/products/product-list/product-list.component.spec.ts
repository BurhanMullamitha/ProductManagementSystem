import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';

import { ProductService } from '../../../services/api/product/product.service';
import { Product } from '../../../models/product';
import { generateObjectId } from '../../../../utils/BsonFactory';
import { ProductListComponent } from './product-list.component';

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productService: ProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ ProductListComponent, HttpClientTestingModule ],
      providers: [ ProductService ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch all products on init', () => {
    const products: Product[] = [
      { id: generateObjectId(), name: 'Product 1', description: 'Product 1 Details', categoryId: generateObjectId(), price: 1000 },
      { id: generateObjectId(), name: 'Product 2', description: 'Product 2 Details', categoryId: generateObjectId(), price: 1400 }
    ];
    spyOn(productService, 'getAllProducts').and.returnValue(of(products));

    component.ngOnInit();

    expect(component.products).toEqual(products);
  });

  it('should log an error message on failed product fetch', () => {
    const consoleSpy = spyOn(console, 'error');
    spyOn(productService, 'getAllProducts').and.returnValue(throwError(() => new Error('Test Error')));

    component.ngOnInit();

    expect(consoleSpy).toHaveBeenCalledWith('There was an error while fetching the products', new Error('Test Error'));
  });

  it('should delete a product', () => {
    const deleteProductSpy = spyOn(productService, 'deleteProduct').and.returnValue(of(undefined));

    component.deleteProduct('1');

    expect(deleteProductSpy).toHaveBeenCalledWith('1');
  });
});
