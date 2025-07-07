import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

export interface LoginRequest {
  username: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  password: string;
}

export interface AuthResponse{
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Authentication`;
  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  private currentUserSubject = new BehaviorSubject<any>(null);

  constructor(private http: HttpClient) {
    if (this.hasToken()) {
      this.loggedIn.next(true);
    }
   }

   register(userData: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}`, userData);
   }

   login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials)
    .pipe(
      map(response => {
        localStorage.setItem('authToken', response.token);

        // Update observables
        this.loggedIn.next(true);

        return response;
      })
    )
   }

   logout(): void {
    localStorage.removeItem('authToken');
    this.loggedIn.next(false);
   }

   isLoggedIn(): boolean {
    return this.loggedIn.value;
  }

  isLoggedIn$(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  getCurrentUser(): any {
    const user = localStorage.getItem('currentUser');
    return user ? JSON.parse(user) : null;
  }

  getCurrentUser$(): Observable<any> {
    return this.currentUserSubject.asObservable();
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('authToken');
  }
}
