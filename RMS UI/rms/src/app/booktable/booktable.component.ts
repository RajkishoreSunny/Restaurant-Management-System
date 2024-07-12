import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { TableService } from '../services/table.service';
import { reservation, table } from '../models/model';
import { PaymentService } from '../services/payment.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-booktable',
  templateUrl: './booktable.component.html',
  styleUrl: './booktable.component.css'
})
export class BooktableComponent implements OnInit {
  constructor(private tableService: TableService, 
    private paymentService: PaymentService,
    private route: Router, private toastr: ToastrService,
    private cdf: ChangeDetectorRef){}
  seatingCapacity: number = 0;
  tableId: number = 0
  ngOnInit(): void {
    this.cdf.detectChanges();
    this.getTables();
  }
  currDate: string = '';
  reservationTable: reservation = {
    customerId: Number(localStorage.getItem('customerId')),
    reservationDateTime: new Date(),
    numberOfPeople: this.seatingCapacity,
    status: true,
    tableId: 0
  }
  tableList: table[] = [];
  getTables(): void
  {
    this.tableService.getAllTables().subscribe({
      next: (res) => {
        this.tableList = res;
      }
    })
  }
  isAvailable(table: any): boolean {
    return table.status.toLowerCase() === 'available';
  }
  getAvailableTables()
  {
    return this.tableService.showAvailableTables().subscribe(
      {
        next: (res) => {
          this.tableList = res;
        }
      }
    )
  }

  navigateToPayment(seatingCapacity: number, status: string)
  {
    if(status === "Available")
    {
      if(this.currDate){
        this.reservationTable.reservationDateTime = new Date(this.currDate);
      }
      this.seatingCapacity = seatingCapacity;
      this.paymentService.updateReservationStatus(true);
      if(seatingCapacity === 4)
      {
        this.tableId = 1;
      }
      else if(seatingCapacity === 6)
      {
        this.tableId = 2;
      }
      else if(seatingCapacity === 7)
      {
        this.tableId = 3;
      }
      else if(seatingCapacity === 2)
      {
        this.tableId = 4;
      }
      else 
      {
        this.tableId = 5;
      }
      this.route.navigate(['/payment'], { queryParams: { 
        customerId: this.reservationTable.customerId,
        reservationDateTime: this.reservationTable.reservationDateTime.toISOString(),
        numberOfPeople: seatingCapacity,
        status: this.reservationTable.status,
        tableId: this.tableId
       }});
       console.log(this.reservationTable.reservationDateTime);
    }
    else
    {
      this.toastr.error("Table is already booked!!");
    }
  }

}
