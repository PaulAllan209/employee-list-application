import { Component } from '@angular/core';
// import { Product } from '@/domain/product';
// import { ProductService } from '@/service/productservice';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import { ApiResponse } from '../../models/api-response.model';


@Component({
  selector: 'app-employee-list',
  imports: [TableModule, CommonModule, ButtonModule, Dialog, InputTextModule, FormsModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent {
  employees!: Employee[];
  loading: boolean = true;
  createEmployeeVisible: boolean = false;

  constructor(private employeeService: EmployeeService) {}

  newEmployee: Employee = {
    firstName: '',
    lastName: '',
    email: '',
    phoneNumber: '',
    position: ''
  };

  showCreateDialog() {
    // This ensures whenever we create new employee the newEmployee object is empty
    this.newEmployee = {
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber: '',
      position: ''
    };

    this.createEmployeeVisible = true;
  }

  ngOnInit(){
    this.fetchEmployees();
  }

  saveEmployee() {
    this.employeeService.createEmployee(this.newEmployee).subscribe({
      next: (createdEmployee) => {
        this.createEmployeeVisible = false;
        this.fetchEmployees(); // refresh the list
      },
      error: (err) => {
        console.error('Error creating employee', err);
      }
    });
  }

  fetchEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (apiResponseData: Employee[]) => {
        this.employees = apiResponseData;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching employees', err);
        this.loading = false;
      }
    });
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: () => {
        this.employees = this.employees.filter(emp => emp.id !== id);
      },
      error: (err) => {
        console.error('Error deleting employee', err);
      }
    });
  }
}
