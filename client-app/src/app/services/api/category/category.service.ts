import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../../../models/category';
import { BehaviorSubject, Observable, of, throwError } from 'rxjs';
import { tap, catchError, map } from 'rxjs/operators';
import { environment } from '../../../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private categoriesSubject: BehaviorSubject<Category[]> = new BehaviorSubject<Category[]>([]);
  categories$ = this.categoriesSubject.asObservable();
  baseUrl: string = environment.baseUrl;

  constructor(private http: HttpClient) {
  }

  getAllCategories(): Observable<Category[]> {
    if (this.categoriesSubject.value.length > 0) {
      return of(this.categoriesSubject.value);
    } else {
      return this.http.get<Category[]>(`${this.baseUrl}/api/Categories`).pipe(
        tap((categories: Category[]) => {
          this.categoriesSubject.next(categories);
        }),
        catchError(error => {
          console.error('There was an error while fetching the categories', error);
          return throwError(() => new Error('There was an error while fetching the categories'));
        })
      );
    }
  }

  getCategory(id: string): Observable<Category> {
    const category = this.categoriesSubject.value.find(category => category.id === id);
    if (category) {
      return of(category);
    } else {
      return this.http.get<Category>(`${this.baseUrl}/api/Categories/${id}`).pipe(
        tap((category: Category) => {
          const currentCategories = this.categoriesSubject.value;
          currentCategories.push(category);
          this.categoriesSubject.next(currentCategories);
        }),
        catchError(error => {
          console.error('There was an error while fetching the category', error);
          return throwError(() => new Error('There was an error while fetching the category'));
        })
      );
    }
  }

  createCategory(category: Category) {
    return this.http.post<Category>(`${this.baseUrl}/api/Categories`, category).pipe(
      tap(() => {
        const currentCategories = this.categoriesSubject.value;
        currentCategories.push(category);
        this.categoriesSubject.next(currentCategories);
      }),
      catchError(error => {
        console.error('There was an error while adding the category', error);
        return throwError(() => new Error('There was an error while adding the category'));
      })
    );
  }

  updateCategory(category: Category) {
    return this.http.put<Category>(`${this.baseUrl}/api/Categories/${category.id}`, category).pipe(
      tap(() => {
        const currentCategories = this.categoriesSubject.value;
        const index = currentCategories.findIndex(c => c.id === category.id);
        currentCategories[index] = category;
        this.categoriesSubject.next(currentCategories);
      }),
      catchError(error => {
        console.error('There was an error while updating the category', error);
        return throwError(() => new Error('There was an error while updating the category'));
      })
    );
  }

  deleteCategory(id: string) {
    return this.http.delete<void>(`${this.baseUrl}/api/Categories/${id}`).pipe(
      tap(() => {
        let currentCategories = this.categoriesSubject.value;
        currentCategories = currentCategories.filter(category => category.id !== id);
        this.categoriesSubject.next(currentCategories);
      }),
      catchError(error => {
        console.error('There was an error while deleting the category', error);
        return throwError(() => new Error('There was an error while deleting the category'));
      })
    );
  }

  getCategoriesArray(): Observable<{text: string, value: string}[]> {
    return this.getAllCategories().pipe(
      map((categories: Category[]) => categories.map(category => ({ text: category.name, value: category.id })))
    )
  }
}
