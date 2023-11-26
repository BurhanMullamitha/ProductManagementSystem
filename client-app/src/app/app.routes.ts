import { Routes } from '@angular/router';
import { AddProductComponent } from './components/products/add-product/add-product.component';
import { UpdateProductComponent } from './components/products/update-product/update-product.component';
import { UpdateCategoryComponent } from './components/categories/update-category/update-category.component';
import { AddCategoryComponent } from './components/categories/add-category/add-category.component';
import { CategoryListComponent } from './components/categories/category-list/category-list.component';
import { NotFoundComponent } from './components/errors/not-found/not-found.component';
import { ProductListComponent } from './components/products/product-list/product-list.component';

export const routes: Routes = [
    { 
        path: '', 
        redirectTo: '/products', 
        pathMatch: 'full' 
    },
    {
        path: 'products',
        component: ProductListComponent
    },
    {
        path: 'products/add',
        component: AddProductComponent
    },
    {
        path: 'products/update/:id',
        component: UpdateProductComponent
    },
    {
        path: 'categories',
        component: CategoryListComponent
    },
    {
        path: 'categories/add',
        component: AddCategoryComponent
    },
    {
        path: 'categories/update/:id',
        component: UpdateCategoryComponent
    },
    { 
        path: '**', 
        component: NotFoundComponent 
    }
];
