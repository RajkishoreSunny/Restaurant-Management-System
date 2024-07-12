import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, map} from 'rxjs';
import { environment } from '../../environments/environment.development';
import { customer, loginModel } from '../models/model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  isLoggedIn = false;
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);
  isLoggedIn$ = this.isLoggedInSubject.asObservable();
  private isLoggedInManagerSubject = new BehaviorSubject<boolean>(false);
  isManagerLoggedIn$ = this.isLoggedInManagerSubject.asObservable();
  constructor(private http: HttpClient) { 
    if (typeof localStorage !== 'undefined') {
      this.checkLoggedInStatus();
    }
    if(typeof localStorage !== 'undefined'){
      this.checkManagerLoggedInStatus();
    }
   }
  login(credentials: loginModel): Observable<any>{
    
    return this.http.post(`${environment.baseUrl}/api/Customer/LoginUser`, 
      {
        email: credentials.email,
        password: credentials.password
      }).pipe(
        map((res: any) => {
          this.isLoggedInSubject.next(true);
          localStorage.setItem('isLoggedIn', 'true');
          return res;
        })
      )
    }

    loginManager(credentials: loginModel): Observable<any>
    {
      return this.http.post(`${environment.baseUrl}/api/Manager/LoginManager`,
      {
        email: credentials.email,
        password: credentials.password
      }).pipe(
        map((res: any) => {
          this.isLoggedInManagerSubject.next(true);
          localStorage.setItem('isManagerLoggedIn', 'true');
          return res;
        })
      )
    }

    logout()
    {
      localStorage.removeItem('isLoggedIn');
      localStorage.removeItem('jwtoken');
      localStorage.removeItem('customerId');
      localStorage.removeItem('customeremail');
      this.isLoggedInSubject.next(false);   
    }

    logoutManager()
    {
      localStorage.removeItem('isManagerLoggedIn');
      localStorage.removeItem('jwtoken');
      localStorage.removeItem('managerName');
      this.isLoggedInManagerSubject.next(false);
    }

    register(customer: customer): Observable<any>
    {
      return this.http.post<any>(`${environment.baseUrl}/api/Customer/AddCustomer`, customer);
    }

    getCustomerDetails(id: number): Observable<any>
    {
      return this.http.get<any>(`${environment.baseUrl}/api/Customer/GetCustomerById?id=${id}`);
    }

    private checkLoggedInStatus(): void {
      const isLoggedIn = localStorage.getItem('isLoggedIn');
      if (isLoggedIn && isLoggedIn === 'true') {
        this.isLoggedInSubject.next(true);
      }
    }
    private checkManagerLoggedInStatus(): void{
      const isLoggedIn = localStorage.getItem('isManagerLoggedIn');
      if(isLoggedIn && isLoggedIn === 'true')
      {
        this.isLoggedInManagerSubject.next(true);
      }
    }
}
