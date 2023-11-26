import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, throwError } from 'rxjs';

import { CategoryListComponent } from './category-list.component';
import { CategoryService } from '../../../services/api/category/category.service';
import { Category } from '../../../models/category';
import { generateObjectId } from '../../../../utils/BsonFactory';

describe('CategoryListComponent', () => {
  let component: CategoryListComponent;
  let fixture: ComponentFixture<CategoryListComponent>;
  let categoryService: CategoryService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ CategoryListComponent, HttpClientTestingModule ],
      providers: [ CategoryService ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CategoryListComponent);
    component = fixture.componentInstance;
    categoryService = TestBed.inject(CategoryService);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch all categories on init', () => {
    const categories: Category[] = [
      { id: generateObjectId(), name: 'Category 1', description: 'Category 1 Details' },
      { id: generateObjectId(), name: 'Category 2', description: 'Category 2 Details' }
    ];
    spyOn(categoryService, 'getAllCategories').and.returnValue(of(categories));

    component.ngOnInit();

    expect(component.categories).toEqual(categories);
  });

  it('should log an error message on failed category fetch', () => {
    const consoleSpy = spyOn(console, 'error');
    spyOn(categoryService, 'getAllCategories').and.returnValue(throwError(() => new Error('Test Error')));

    component.ngOnInit();

    expect(consoleSpy).toHaveBeenCalledWith('There was an error while fetching the categories', new Error('Test Error'));
  });

  it('should delete a category', () => {
    const deleteCategorySpy = spyOn(categoryService, 'deleteCategory').and.returnValue(of(undefined));

    component.deleteCategory('1');

    expect(deleteCategorySpy).toHaveBeenCalledWith('1');
  });
});
