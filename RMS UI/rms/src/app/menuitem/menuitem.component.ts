import { Component, OnInit } from '@angular/core';
import { menu, orders } from '../models/model';
import { ActivatedRoute } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-menuitem',
  templateUrl: './menuitem.component.html',
  styleUrl: './menuitem.component.css'
})
export class MenuitemComponent implements OnInit {

  constructor(private aroute: ActivatedRoute,
    private restaurantService: RestaurantService,
    private toastr: ToastrService,
    private route: Router,
    private loginService: LoginService) { }
  menuId: number = 0;
  isLoggedIn: boolean = false;
  orderId: number = 0;
  price: number = 0;
  currDate: Date = new Date();
  quantity: number = 0;
  menuItem: menu = {
    menuId: 0,
    categoryId: 0,
    name: '',
    description: '',
    price: 0,
    itemImg: '',
    rating: 0
  };
  orderItems: orders = {
    customerId: 0,
    orderDate: this.currDate,
    totalPrice: 0,
    menuId: 0,
    quantity: 0
  };
  ngOnInit(): void {
    const idParam = this.aroute.snapshot.paramMap.get('id');
    if (idParam !== null) {
      this.menuId = +idParam;
    } else {
      console.error("Item ID parameter is null");
    }
    this.restaurantService.getMenuById(this.menuId).subscribe(
      {
        next: (res) => {
          this.menuItem = res;
        }
      }
    )
  }
  addOrder(menuItem: menu) {
    if(this.quantity <= 0)
      {
        this.toastr.error("Please select atleast 1 item!");
        return;
      }
    this.loginService.isLoggedIn$.subscribe({ next: isLogin => this.isLoggedIn = isLogin })
    if (this.isLoggedIn === true) {
      this.orderItems.customerId = Number(localStorage.getItem('customerId'));
      this.orderItems.menuId = menuItem.menuId;
      this.orderItems.quantity = this.quantity;
      this.orderItems.orderDate = this.currDate;
      this.orderItems.totalPrice = (menuItem.price * this.quantity);
      this.restaurantService.addOrder(this.orderItems).subscribe(
        {
          next: (res) => {
            this.toastr.success('Routing to Payment Page', 'Added Succesfully!');
            this.orderId = res.orderId;
            this.price = res.totalPrice;
            setTimeout(() => {
              this.route.navigate(['/payment'], { queryParams: { id: this.orderId, price: this.price } });
            }, 1000)
          },
          error: () => {
            this.toastr.error('Session Expired Please Login Again!');
            setTimeout(() => {
              this.route.navigate(['/login']);
            }, 3000)
          }
        }
      )
    }
    else
    {
      this.toastr.error('Please Login First');
      this.route.navigate(['/login']);
    }
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
  increase() {
    this.quantity++;
  }
  decrease() {
    if (this.quantity > 0) {
      this.quantity--;
    }
  }
}
