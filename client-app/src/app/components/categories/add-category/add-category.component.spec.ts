import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

import { AddCategoryComponent } from './add-category.component';
import { CategoryService } from '../../../services/api/category/category.service';
import { FormService } from '../../../services/form/category/form.service';
import { Category } from '../../../models/category';
import { generateObjectId } from '../../../../utils/BsonFactory';

describe('AddCategoryComponent', () => {
  let component: AddCategoryComponent;
  let fixture: ComponentFixture<AddCategoryComponent>;
  let categoryService: CategoryService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ AddCategoryComponent, HttpClientTestingModule, ReactiveFormsModule ],
      providers: [ CategoryService, FormService ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddCategoryComponent);
    component = fixture.componentInstance;
    categoryService = TestBed.inject(CategoryService);
    router = TestBed.inject(Router);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should navigate to categories page on successful category creation', () => {
    const navigateSpy = spyOn(router, 'navigate');
    const dummyCategory: Category = { id: generateObjectId(), name: 'Category 3', description: 'Category 3 Details' };
    const createCategorySpy = spyOn(categoryService, 'createCategory').and.returnValue(of(dummyCategory));

    component.categoryForm.setValue(dummyCategory);
    component.onSubmit();

    expect(createCategorySpy).toHaveBeenCalled();
    expect(navigateSpy).toHaveBeenCalledWith(['/categories']);
  });

  it('should log an error message on failed category creation', () => {
    const consoleSpy = spyOn(console, 'error');
    const createCategorySpy = spyOn(categoryService, 'createCategory').and.returnValue(throwError(() => new Error('Test Error')));

    component.categoryForm.setValue({ id: generateObjectId(), name: 'Test Category', description: 'Test Description' });
    component.onSubmit();

    expect(createCategorySpy).toHaveBeenCalled();
    expect(consoleSpy).toHaveBeenCalledWith('There was an error while adding the category', new Error('Test Error'));
  });
});