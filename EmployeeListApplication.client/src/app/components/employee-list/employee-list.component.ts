import { Component } from '@angular/core';
// import { Product } from '@/domain/product';
// import { ProductService } from '@/service/productservice';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { Employee } from '../../models/Employee.Model';

@Component({
  selector: 'app-employee-list',
  imports: [TableModule, CommonModule, ButtonModule],
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent {
  employees!: Employee[];

  ngOnInit(){
    this.employees = [
      {
        id: '06CF26B5-5A8A-4673-70C4-08DDBC359C69',
        FirstName: 'Paul',
        LastName: 'Dela',
        Email: 'dpaul@gmail.com',
        PhoneNumber: '09216933288',
        Position: 'c# dev'
      },
      {
        id: '06CF26B5-5A8A-4673-70C4-08DDBC359C69',
        FirstName: 'Paul',
        LastName: 'Dela',
        Email: 'dpaul@gmail.com',
        PhoneNumber: '09216933288',
        Position: 'c# dev'
      },
      {
        id: '06CF26B5-5A8A-4673-70C4-08DDBC359C69',
        FirstName: 'Paul',
        LastName: 'Dela',
        Email: 'dpaul@gmail.com',
        PhoneNumber: '09216933288',
        Position: 'c# dev'
      },
      {
        id: '06CF26B5-5A8A-4673-70C4-08DDBC359C69',
        FirstName: 'Paul',
        LastName: 'Dela',
        Email: 'dpaul@gmail.com',
        PhoneNumber: '09216933288',
        Position: 'c# dev'
      }
    ];
  }
}
