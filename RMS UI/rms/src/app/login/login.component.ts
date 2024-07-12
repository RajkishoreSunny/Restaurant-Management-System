import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { loginModel } from '../models/model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  isLoggedIn = false;
  isPasswordVisible: boolean = false;
  constructor(private loginService: LoginService, private toastr: ToastrService, private router: Router) {}
  loginForm = new FormGroup({
    email: new FormControl<any>('', [Validators.required, Validators.email]),
    password: new FormControl<any>('', [Validators.required, Validators.maxLength(20), Validators.minLength(7)])
  });
  login()
  {
    const email = this.loginForm.get('email')?.value;
    const password = this.loginForm.get('password')?.value;

    const credentials: loginModel = { email, password };

    this.loginService.login(credentials).subscribe(
      {
        next: (res) => {
          const response = res.response;
          const token = res.token;
          this.toastr.success('Routing to Menu Page!!', 'Login Succefull!!');
          localStorage.setItem('customerId', response.customerId);
          localStorage.setItem('jwtoken', token);
          localStorage.setItem('customeremail', response.customerEmail);
          this.loginService.isLoggedIn = true;
          setTimeout(() => {
            this.router.navigate(['/menucategory']);
          }, 3000);
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('UnAuthorized!!');
        }
      }
    )
  }
  togglePasswordVisibility(){
    this.isPasswordVisible = !this.isPasswordVisible;
  }

}
