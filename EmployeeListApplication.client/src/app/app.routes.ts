import { Routes } from '@angular/router';
import { AuthGuard } from './services/auth-guard';

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./components/employee-list/employee-list.component').then(m => m.EmployeeListComponent),
        canActivate: [AuthGuard]
    },
    {
        path: 'login',
        loadComponent: () => import('./components/login-page/login-page.component').then(m => m.LoginPageComponent),
        title: 'Login'
    }
];
