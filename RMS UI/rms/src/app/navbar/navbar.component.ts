import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { menu, menuCategory } from '../models/model';
import { LoginService } from '../services/login.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
  constructor(private restaurantService: RestaurantService, 
    private loginService: LoginService, 
    private route: Router,
    private toastr: ToastrService){}
  isLogin = false;
  isManagerLoggedIn = false;
  ngOnInit(): void {
    this.loginService.isLoggedIn$.subscribe(isLoggedIn => {
      this.isLogin = isLoggedIn;
    })
    this.loginService.isManagerLoggedIn$.subscribe(isLoggedIn => {
      this.isManagerLoggedIn = isLoggedIn;
    })
    this.fetchCategory();
  }
  fetchMenuItem(categoryId: number)
  {
    this.route.navigate(['/menuitem'], { queryParams: { categoryId } });
  }
  categoryArray: menuCategory[] = [];
  fetchCategory()
  {
    this.restaurantService.fetchCategory().subscribe(
      {
        next: (res) => {
          this.categoryArray = res;
        }
      }
    )
  }
  logout()
  {
    this.isLogin = false;
    this.loginService.isLoggedIn = false;
    this.loginService.logout();
    this.toastr.success('Navigating to Login Page', 'Logged Out Successfully');
      setTimeout(() => {
        this.route.navigate(['login']);
      }, 3000);
  }
  logoutManager()
  {
    this.isManagerLoggedIn = false;
    this.loginService.logoutManager();
    this.toastr.success('Navigating to Login Page', 'Logged Out Successfully');
      setTimeout(() => {
        this.route.navigate(['mlogin']);
      }, 3000);
  }

  bookTable()
  {
    if(this.isLogin === true)
    {
      this.route.navigate(['/booktable']);
    }
    else
    {
      this.toastr.error("Please Login");
      this.route.navigate(['/login']);
    }
  }

  //Search Functionality
  searchTerm: string = "";
  searchResults: menu[] = [];

  handleSearch()
  {
    this.restaurantService.fetchMenuItem(this.searchTerm).subscribe({
      next: (res:any) => 
      {
        if(res.length > 0)
        {
          this.searchResults = res;
        }
      },
      error: (err) => 
      {
        console.error(err);
      }
    })
  }
  handleInputChange(event: any) {
    const inputValue = event.target.value;
    this.searchTerm = inputValue;
    if (inputValue.length > 0) {
      this.handleSearch();
    } else {
      this.categoryArray;
    }
  }

  getToItem(menuId: number)
  {
    this.route.navigate(['/menuitem', menuId]);
  }
  }
