import { Component, inject } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Card, CardModule } from 'primeng/card';
import { ToastModule } from 'primeng/toast';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { Employee } from '../../models/employee.model';
import { MessageModule } from 'primeng/message';
import { EmployeeService } from '../../services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-single',
  imports: [CardModule, ToastModule, MessageModule, ButtonModule, InputTextModule, FormsModule, CommonModule],
  templateUrl: './employee-single.component.html',
  styleUrl: './employee-single.component.scss'
})
export class EmployeeSingleComponent {
  employeeId: string = '';
  employee: Employee | null = null;
  loading: boolean = false;
  searched: boolean = false;

  employeeService = inject(EmployeeService);
  router = inject(Router);

  constructor(private messageService: MessageService) {}

  searchEmployee(): void {
    if (!this.employeeId.trim()) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Warning',
        detail: 'Please enter an employee ID'
      });
      return;
    }

    this.loading = true;
    this.searched = false;
    this.employee = null;

    // Simulate API call - replace with your actual service call
    this.employeeService.getEmployeeById(this.employeeId).subscribe({
      next: (response: Employee) => {
        this.employee = response;
        this.loading = false;
        this.searched = true;

        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Employee found successfully'
        });
      },
      error: (err) => {
        this.messageService.add({ 
          severity: 'error', 
          summary: 'Failed to find employee', 
          detail: err.error?.message || 'Invalid Employee ID or Not Found', 
          life: 3000
        });
        this.loading = false;
      }
    });
  }

  goToMainPage() {
      this.router.navigate(['/']);
  }
}
