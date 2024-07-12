import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { about } from '../models/model';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrl: './about.component.css'
})
export class AboutComponent implements OnInit {
  constructor(private restaurantService: RestaurantService){}
  ngOnInit(): void {
    this.getRestaurantDetails();
  }
  restaurantDetails: about[] = [];

  getRestaurantDetails(): void
  {
    this.restaurantService.fetchRestaurantDetails().subscribe({
      next: (res: any) => 
      {
        this.restaurantDetails = res;
      }
    })
  }
}
