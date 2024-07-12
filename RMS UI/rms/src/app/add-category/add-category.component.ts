import { Component } from '@angular/core';
import { menuCategory } from '../models/model';
import { RestaurantService } from '../services/restaurant.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.css'
})
export class AddCategoryComponent {
  constructor(private restaurantService: RestaurantService, private toastr: ToastrService){}
category: menuCategory = {
  categoryName: '',
  categoryId: 0,
  description: '',
  categoryImg: ''
}
categoryImg!: File;
id: number = 0;
onSubmit(formData: any)
{
  this.restaurantService.addCategoryMenu(formData).subscribe({
    next: (res) => {
      this.toastr.success('Added Succesfully');
      this.id = res.categoryId;
    }
  })
}
onImageSelected(event: any)
{
  this.categoryImg = event.target.files[0];
}
uploadImage()
{
  this.restaurantService.addImage(this.id, this.categoryImg).subscribe({
    next: () => {
      this.toastr.success('Image Uploaded Successfully');
    },
    error: (err) => {
      console.error(err);
    }
  })
}
}
