import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CategoryService } from './category.service';
import { Category } from '../../../models/category';

describe('CategoryService', () => {
  let service: CategoryService;
  let httpMock: HttpTestingController;

  beforeEach(async() => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CategoryService]
    }).compileComponents();

    service = TestBed.inject(CategoryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Ensure that there are no outstanding requests.
  });

  
  it('should get all categories', () => {
    const dummyCategories: Category[] = [
      { id: '1', name: 'Category 1', description: 'Category 1 Details' },
      { id: '2', name: 'Category 2', description: 'Category 2 Details' }
    ];

    service.getAllCategories().subscribe(categories => {
      expect(categories.length).toBe(2);
      expect(categories).toEqual(dummyCategories);
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyCategories);
  });

  it('should handle error for getAllCategories', () => {
    service.getAllCategories().subscribe({
      next: categories => expect(categories.length).toBe(0),
      error: error => expect(error).toEqual(new Error('There was an error while fetching the categories'))
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories`);
    expect(req.request.method).toBe('GET');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should get a category by id', () => {
    const dummyCategory: Category = { id: '1', name: 'Category 1', description: 'Category 1 Details' };

    service.getCategory(dummyCategory.id).subscribe(category => {
      expect(category).toEqual(dummyCategory);
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/${dummyCategory.id}`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyCategory);
  });

  it('should handle error for getCategory', () => {
    service.getCategory('1').subscribe({
      next: category => expect(category).toEqual({} as Category),
      error: error => expect(error).toEqual(new Error('There was an error while fetching the category'))
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/1`);
    expect(req.request.method).toBe('GET');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should create a category successfully in createCategory', () => {
    const dummyCategory: Category = { id: '3', name: 'Category 3', description: 'Category 3 Details' };

    service.createCategory(dummyCategory).subscribe(category => {
      expect(category).toEqual(dummyCategory);
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories`);
    expect(req.request.method).toBe('POST');
    req.flush(dummyCategory);
  });

  it('should handle error for createCategory', () => {
    const dummyCategory: Category = { id: '3', name: 'Category 3', description: 'Category 3 Details' };

    service.createCategory(dummyCategory).subscribe({
      next: category => expect(category).toEqual({} as Category),
      error: error => expect(error).toEqual(new Error('There was an error while adding the category'))
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories`);
    expect(req.request.method).toBe('POST');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should update a category successfully in updateCategory', () => {
    const dummyCategory: Category = { id: '1', name: 'Updated Category 1', description: 'Updated Category 1 Details' };

    service.updateCategory(dummyCategory).subscribe(category => {
      expect(category).toEqual(dummyCategory);
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(dummyCategory);
  });

  it('should handle error for updateCategory', () => {
    const dummyCategory: Category = { id: '1', name: 'Updated Category 1', description: 'Updated Category 1 Details' };

    service.updateCategory(dummyCategory).subscribe({
      next: category => expect(category).toEqual({} as Category),
      error: error => expect(error).toEqual(new Error('There was an error while updating the category'))
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/1`);
    expect(req.request.method).toBe('PUT');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should delete a category successfully in deleteCategory', () => {
    service.deleteCategory('1').subscribe(res => {
      expect(res).toBeNull();
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush(null);
  });

  it('should handle error for deleteCategory', () => {
    service.deleteCategory('1').subscribe({
      next: res => expect(res).toBeNull(),
      error: error => expect(error).toEqual(new Error('There was an error while deleting the category'))
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush('Error', { status: 500, statusText: 'Server Error' });
  });

  it('should get categories array successfully in getCategoriesArray', () => {
    const dummyCategories: Category[] = [
      { id: '1', name: 'Category 1', description: 'Category 1 details' },
      { id: '2', name: 'Category 2', description: 'Category 2 details' }
    ];

    const expectedCategoriesArray = [
      { value: '1', text: 'Category 1' },
      { value: '2', text: 'Category 2' }
    ];

    service.getCategoriesArray().subscribe(categoriesArray => {
      expect(categoriesArray.length).toBe(2);
      expect(categoriesArray).toEqual(expectedCategoriesArray);
    });

    const req = httpMock.expectOne(`${service.baseUrl}/api/Categories`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyCategories);
  });
});