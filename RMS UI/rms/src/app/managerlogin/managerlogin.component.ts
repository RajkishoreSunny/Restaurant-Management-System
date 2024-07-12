import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { loginModel } from '../models/model';

@Component({
  selector: 'app-managerlogin',
  templateUrl: './managerlogin.component.html',
  styleUrl: './managerlogin.component.css'
})
export class ManagerloginComponent {
  isLoggedIn = false;
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

    this.loginService.loginManager(credentials).subscribe(
      {
        next: (res) => {
          const token = res.token;
          const name = res.response.managerName;
          this.toastr.success('Welcome Manager!!', 'Login Succefull!!');
          localStorage.setItem('managerName', name);
          localStorage.setItem('jwtoken', token);
          setTimeout(() => {
            this.router.navigate(['/rmanager']);
          }, 3000);
        },
        error: (err) => {
          console.log(err);
          this.toastr.error('UnAuthorized!!');
        }
      }
    )
  }

}
