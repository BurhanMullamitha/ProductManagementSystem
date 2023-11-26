import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

import { AddProductComponent } from './add-product.component';
import { ProductService } from '../../../services/api/product/product.service';
import { FormService } from '../../../services/form/product/form.service';
import { Product } from '../../../models/product';
import { generateObjectId } from '../../../../utils/BsonFactory';

describe('AddProductComponent', () => {
  let component: AddProductComponent;
  let fixture: ComponentFixture<AddProductComponent>;
  let productService: ProductService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AddProductComponent, HttpClientTestingModule, ReactiveFormsModule ],
      providers: [ ProductService, FormService ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddProductComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to products page on successful product creation', () => {
    const navigateSpy = spyOn(router, 'navigate');
    const dummyProduct: Product = { id: generateObjectId(), name: 'Product 3', description: 'Product 3 Details', categoryId: generateObjectId(), price: 1000, color: '' };
    const createProductSpy = spyOn(productService, 'createProduct').and.returnValue(of(dummyProduct));

    component.productForm.setValue(dummyProduct);
    component.onSubmit();

    expect(createProductSpy).toHaveBeenCalled();
    expect(navigateSpy).toHaveBeenCalledWith(['/products']);
  });

  it('should log an error message on failed product creation', () => {
    const consoleSpy = spyOn(console, 'error');
    const dummyProduct: Product = { id: generateObjectId(), name: 'Product 3', description: 'Product 3 Details', categoryId: generateObjectId(), price: 98, color: '' };
    const createProductSpy = spyOn(productService, 'createProduct').and.returnValue(throwError(() => new Error('Test Error')));

    component.productForm.setValue(dummyProduct);
    component.onSubmit();

    expect(createProductSpy).toHaveBeenCalled();
    expect(consoleSpy).toHaveBeenCalledWith('There was an error while adding the product', new Error('Test Error'));
  });
});
