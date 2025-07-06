import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { ButtonModule } from 'primeng/button';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';



@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ButtonModule, EmployeeListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
}
