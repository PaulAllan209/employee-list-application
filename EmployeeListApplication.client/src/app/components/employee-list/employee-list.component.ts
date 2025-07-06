import { Component } from '@angular/core';
// import { Product } from '@/domain/product';
// import { ProductService } from '@/service/productservice';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { CommonModule } from '@angular/common';
import { Employee } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import { ApiResponse } from '../../models/api-response.model';


@Component({
  selector: 'app-employee-list',
  imports: [TableModule, CommonModule, ButtonModule, Dialog, InputTextModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent {
  employees!: Employee[];
  loading: boolean = true;
  createEmployeeVisible: boolean = false;

  constructor(private employeeService: EmployeeService) {}

  showCreateDialog() {
    this.createEmployeeVisible = true;
  }

  ngOnInit(){
    this.fetchEmployees();
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
}
