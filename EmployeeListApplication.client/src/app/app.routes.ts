import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/employee-list/employee-list.component').then(m => m.EmployeeListComponent)
    },
    {
        path: 'employee-list',
        redirectTo: '/',
        pathMatch: 'full'
    }
];
