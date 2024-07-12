import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { menu, orders } from '../models/model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-pastorders',
  templateUrl: './pastorders.component.html',
  styleUrl: './pastorders.component.css'
})
export class PastordersComponent implements OnInit {
  constructor(private restaurantService: RestaurantService, private loginService: LoginService,
    private toastr: ToastrService,
    private route: Router){}
  ngOnInit(): void {
    this.fetchListOfOrders();
  }
  obj: object = {
    image: '',
    itemName: ''
  }
  listOfPastOrders: orders[] = []
  fetchListOfOrders()
  {
    const custId = Number(localStorage.getItem('customerId'));
    this.restaurantService.fetchListOfOrders(custId).subscribe({
      next: (res) => {
          this.listOfPastOrders = res;
          if(this.listOfPastOrders.length === 0)
          {
            this.toastr.error('Oh No! No Orders till now!');
            setTimeout(() => {
              this.toastr.success('Lets Order Some Items');
            }, 2000);
            setTimeout(() => {
              this.toastr.success('Taking you to some famous cuisines! Sit tight and let\'s roll!');
            },4000)
            setTimeout(() => {
              this.route.navigate(['/menucategory']);
            }, 6000)
          }
          this.listOfPastOrders.forEach(order => this.fetchMenuDetails(order));
        },
        error: () => {
          this.toastr.error('Session expired! Please Login again!');
          this.loginService.logout();
          this.route.navigate(['/login']);
        }
        });
  }

  fetchMenuDetails(order: orders): void {
    this.restaurantService.getMenuById(order.menuId).subscribe({
      next: (menuDetails) => {
        order.itemImg = menuDetails.itemImg;
        order.name = menuDetails.name;
      },
      error: () => {
        console.error(`Could not fetch menu details for menuId ${order.menuId}`);
      }
    });
  }
  getImg(img: string): string {
    if (img) {
      const base64Img = img;
      return `data:image/png;base64,${base64Img}`;
    }
    else {
      return '';
    }
  }

}
