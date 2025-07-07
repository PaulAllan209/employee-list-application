import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { Employee } from '../models/employee.model';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = `${environment.apiUrl}/Employee`;

    private getAuthHeaders(): HttpHeaders {
        const token = this.authService.getToken();
        return new HttpHeaders({
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'application/json'
        });
    }

    constructor(private http: HttpClient, private authService: AuthService){}

    getEmployees(): Observable<Employee[]> {
        const headers = this.getAuthHeaders();
        return this.http.get<Employee[]>(this.apiUrl, { headers });
    }

    getEmployeeById(id: string): Observable<Employee> {
        const headers = this.getAuthHeaders();
        return this.http.get<Employee>(`${this.apiUrl}/${id}`, { headers });
    }

    createEmployee(employee: Employee): Observable<Employee> {
        const headers = this.getAuthHeaders();
        return this.http.post<Employee>(this.apiUrl, employee, { headers });
    }

    updateEmployee(id: string, employeeChangedFields: Partial<Employee>): Observable<void> {
        const patchOperations = this.createPatchOperations(employeeChangedFields);
        const headers = this.getAuthHeaders().set('Content-Type', 'application/json-patch+json');
        return this.http.patch<void>(`${this.apiUrl}/${id}`, patchOperations, { headers });
    }

    private createPatchOperations(updates: Partial<Employee>): any[] {
    return Object.entries(updates)
        .filter(([_, value]) => value !== undefined && value !== null)
        .map(([key, value]) => ({
            op: 'replace',
            path: `/${key}`,
            value: value
        }));
}

    deleteEmployee(id: string): Observable<void> {
        const headers = this.getAuthHeaders();
        return this.http.delete<void>(`${this.apiUrl}/${id}`, { headers });
    }
}