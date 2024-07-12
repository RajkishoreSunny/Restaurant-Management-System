import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { customer } from '../models/model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
constructor(private loginService: LoginService, private toastr: ToastrService, private route: Router){}
registerForm = new FormGroup({
  customerName: new FormControl<any>('', [Validators.required, Validators.minLength(2), 
    Validators.pattern("[a-zA-Z].*")]),
  customerEmail: new FormControl<any>('', [Validators.required, Validators.email]),
  customerPhone: new FormControl<any>('', [Validators.minLength(10)]),
  customerAddress: new FormControl<any>('', [Validators.required]),
  password: new FormControl<any>('', [Validators.required, Validators.minLength(5), Validators.maxLength(15)])
})
register()
{
  const custData: customer = {
    customerName: this.registerForm.value.customerName,
    customerEmail: this.registerForm.value.customerEmail,
    customerPhone: this.registerForm.value.customerPhone,
    customerAddress: this.registerForm.value.customerAddress,
    password: this.registerForm.value.password
  }
  this.loginService.register(custData).subscribe({
    next: () => {
      this.toastr.success('Taking You to Login page', 'Registered Successfully!');
      setTimeout(() => {
        this.route.navigate(['/login']);
      }, 3000);
    },
    error: () => {
      this.toastr.error('Couldnot Register');
    }
  })
}
get customerName(): FormControl{
  return this.registerForm.get('customerName') as FormControl;
}
get customerEmail(): FormControl{
  return this.registerForm.get('customerEmail') as FormControl;
}
get customerAddress(): FormControl{
  return this.registerForm.get('customerAddress') as FormControl;
}
get customerPhone(): FormControl{
  return this.registerForm.get('customerPhone') as FormControl;
}
get password(): FormControl{
  return this.registerForm.get('password') as FormControl;
}
}
