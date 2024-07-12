import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { ActivatedRoute, Router } from '@angular/router';
import { menu } from '../models/model';

@Component({
  selector: 'app-menuitems',
  templateUrl: './menuitems.component.html',
  styleUrl: './menuitems.component.css'
})
export class MenuitemsComponent implements OnInit {
  constructor(private restaurantService: RestaurantService,
     private activatedRoute: ActivatedRoute,
     private route: Router){
  }
  categoryId : number = 0;
  menuItems: menu[] = [];
  categoryIdparam: number = 0;
  
  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.categoryIdparam = params['categoryId'];
      if(this.categoryIdparam !== undefined)
      {
        this.categoryId = Number(this.categoryIdparam);
        this.fetchItem();
      }
    });
    this.fetchItem();
  }
  
  fetchItem()
  {
    this.restaurantService.fetchMenuItems(this.categoryId).subscribe(
      {
        next: (res) => 
        {
          this.menuItems = res;
        },
        error: (err) => 
        {
          console.log(err);
        }
      }
    )
  }

  getImg(img: string) : string
  {
    if(img)
    {
      const base64Img = img;
      return `data:image/png;base64,${base64Img}`;
    }
    else
    {
      return '';
    }
  }
  routeToMenuItem(itemId: number)
  {
    this.route.navigate(['menuitem', itemId]);
  }
}
