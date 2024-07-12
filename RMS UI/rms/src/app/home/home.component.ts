import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { menu, menuCategory } from '../models/model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  constructor(private restaurantService: RestaurantService, private route: Router){
  }
  categoryItems: menuCategory[] = [];
  ngOnInit(): void {
    this.fetchCategoryItems();
  }
  fetchCategoryItems()
  {
    this.restaurantService.fetchCategory().subscribe(
      {
        next: (res) => {
          this.categoryItems = res;
          console.log(this.categoryItems);
        },
        error: (err) => {
          console.log(err);
        }
      }
    );
  }
  getImagePath(imageName: string):  string 
  {
    if(imageName)
    {
      const base64Img = imageName;
      return `data:image/png;base64,${base64Img}`
    }
    else
    {
      return '';
    }
  }
  routeToMenuItem(categoryId: number)
  {
    this.route.navigate(['/menuitem'], { queryParams: { categoryId } });
  }
}
