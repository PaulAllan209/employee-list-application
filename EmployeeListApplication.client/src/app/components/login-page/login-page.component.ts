import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { MessageModule } from 'primeng/message';
import { ToastModule } from 'primeng/toast';
import { PasswordModule } from 'primeng/password';
import { FloatLabelModule } from 'primeng/floatlabel';
import { MessageService } from 'primeng/api';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { AuthService, LoginRequest } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  imports: [FormsModule, InputTextModule, MessageModule, ToastModule, ButtonModule, PasswordModule, FloatLabelModule],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  messageService = inject(MessageService);
  authService = inject(AuthService);
  router = inject(Router);

    loginData: LoginRequest = {
        username: '',
        password: ''
    };

    onSubmit(form: any) {
        if (form.valid) {
            this.authService.login(this.loginData).subscribe({
              next: (response) => {
                this.messageService.add({ 
                  severity: 'success', 
                  summary: 'Success', 
                  detail: 'Welcome!', 
                  life: 3000
                });
                this.router.navigate(['/']);

              },
              error: (error) => {
                this.messageService.add({ 
                  severity: 'error', 
                  summary: 'Login Failed', 
                  detail: error.error?.message || 'Invalid username or password', 
                  life: 3000
                });
              }
            });

            console.log('Login attempt:', this.loginData);
            
            // Reset form after successful login
            form.resetForm();
            this.loginData = { username: '', password: '' };
        } else {
            this.messageService.add({ 
                severity: 'error', 
                summary: 'Error', 
                detail: 'Please fill in all required fields', 
                life: 3000 
            });
        }
    }

    register(form: any) {
      if(form.valid) {
        this.authService.register(this.loginData).subscribe({
          next: (response) => {
            this.messageService.add({ 
              severity: 'success', 
              summary: 'Register Success', 
              detail: 'Successfully Registered!', 
              life: 3000
          });
        },
          error: (error) => {
            this.messageService.add({ 
              severity: 'error', 
              summary: 'Register Failed', 
              detail: error.error?.message || 'Invalid username or password', 
              life: 3000
            });
          }
      });
      } else {
          this.messageService.add({ 
            severity: 'error', 
            summary: 'Error', 
            detail: 'Please fill in all required fields', 
            life: 3000 
          });
        }

      form.resetForm();
      this.loginData = { username: '', password: '' };
    }

    
}
