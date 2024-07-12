import { Component } from '@angular/core';
import { menu } from '../models/model';
import { RestaurantService } from '../services/restaurant.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-menu',
  templateUrl: './add-menu.component.html',
  styleUrl: './add-menu.component.css'
})
export class AddMenuComponent {

  constructor(private restaurantService: RestaurantService, private toastr: ToastrService){}
  menuItems: menu = {
    menuId: 0,
    categoryId: 0,
    name: '',
    description: '',
    itemImg: '',
    price: 0,
    rating: 0
  }
  menuId: number = 0;
  itemImg!: File;

  onSubmit(formValue: any)
  {
    this.restaurantService.addMenuItem(formValue).subscribe({
      next: (res) => {
        this.menuId = res.menuId;
        this.toastr.success('Added Succesfully');
      }
      ,
      error: (err) => {
        console.log(err);
      }
    })
  }
  onImageSelected(event: any)
  {
    this.itemImg = event.target.files[0];
  }
  uploadImage()
  {
    this.restaurantService.addItemImage(this.menuId, this.itemImg).subscribe({
      next: (res) => {
        if(res === true)
        {
          this.toastr.success("Image Uploaded Successfully");
        }
        else
        {
          this.toastr.error("Couldn't Upload");
        }
      }
    })
  }
}
