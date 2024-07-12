import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { menuCategory } from '../models/model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menucategory',
  templateUrl: './menucategory.component.html',
  styleUrl: './menucategory.component.css'
})
export class MenucategoryComponent implements OnInit {
 constructor(private restaurantService: RestaurantService, private router: Router) {}
  ngOnInit(): void {
    this.fetchCategoryItems();
  }
  categoryItems: menuCategory[] = [];
 fetchCategoryItems()
  {
    this.restaurantService.fetchCategory().subscribe(
      {
        next: (res) => {
          this.categoryItems = res;
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
  routeToItem(categoryId: number)
  {
    this.router.navigate(['/menuitem'], { queryParams: { categoryId } });
  }
}
