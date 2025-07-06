import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = `${environment.apiUrl}/Employee`;

    constructor(private http: HttpClient){}

    getEmployees(): Observable<Employee[]> {
        return this.http.get<Employee[]>(this.apiUrl);
    }

    getEmployeeById(id: string): Observable<Employee> {
        return this.http.get<Employee>(`${this.apiUrl}/${id}`);
    }

    createEmployee(employee: Employee): Observable<Employee> {
        return this.http.post<Employee>(this.apiUrl, employee);
    }

    updateEmployee(id: string, employeeChangedFields: Partial<Employee>): Observable<void> {
        const patchOperations = this.createPatchOperations(employeeChangedFields);
        const headers = { 'Content-Type': 'application/json-patch+json' };
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
        return this.http.delete<void>(`${this.apiUrl}/${id}`);
    }
}