import { Component, OnInit } from '@angular/core';
import { TableService } from '../services/table.service';
import { menuCategory, table } from '../models/model';
import { ToastrService } from 'ngx-toastr';
import { RestaurantService } from '../services/restaurant.service';

@Component({
  selector: 'app-restaurantmanager',
  templateUrl: './restaurantmanager.component.html',
  styleUrl: './restaurantmanager.component.css'
})
export class RestaurantmanagerComponent implements OnInit {
  constructor(private tableService: TableService, 
    private toastr: ToastrService,
    private restaurantService: RestaurantService){}
  name: string | null = '';
  ngOnInit(): void {
    this.tableService.getAllTables().subscribe({
      next: (res) => {
        this.tables = res;
      }
    })
    this.name = localStorage.getItem('managerName');
    this.getMenuCategory();
  }
  tables: table[] = [];
  categories: menuCategory[] = [];
  updateStatus(id: number)
  {
    this.tableService.updateStatus(id, 'Available').subscribe({
      next: (res) => 
      {
        if(res === true)
        {
          this.toastr.success('Status Updated');
        }
      },
      error: () => 
      {
        this.toastr.error("Couldnot Update");
      }
    })
  }
  getMenuCategory()
  {
    this.restaurantService.fetchCategory().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: (err) => {
        console.error(err);
      }
    })
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

  selectedTable: table = {
    tableId: 0,
    seatingCapacity: 0,
    status: ''
  };

  selectTable(table: table) {
    this.selectedTable = table
  }

  updateTable()
  {
    let id = Number(this.selectedTable?.tableId);
    this.tableService.updateTable(id, this.selectedTable).subscribe({
      next: (res) => {
        if(res.status)
        {
          this.toastr.success('Updated Succesfully');
        }
      }
    })
  }
  cancelUpdate()
  {
    this.selectedTable = {
      tableId: 0,
      seatingCapacity: 0,
      status: ''
    };
  }
}
