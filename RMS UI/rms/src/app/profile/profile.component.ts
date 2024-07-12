import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { customer, getCustomerDetails } from '../models/model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent implements OnInit {
constructor(private loginService: LoginService, private toastr: ToastrService){}
  ngOnInit(): void {
    this.getDetails();
  }
  customer: getCustomerDetails = {
    customerName: "",
    customerEmail: "",
    customerAddress: "",
    customerPhone: ""
  }
  getDetails()
  {
    const id = Number(localStorage.getItem('customerId'));
    this.loginService.getCustomerDetails(id).subscribe(
      {
        next: (res) => 
        {
          this.customer = res;
        },
        error: () => {
          this.toastr.error('CouldNot Fetch!');
        }
      }
    )
  }
}
