import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { PaymentService } from '../services/payment.service';
import { ToastrService } from 'ngx-toastr';
import { orders, payment, reservation } from '../models/model';
import { RestaurantService } from '../services/restaurant.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { TableService } from '../services/table.service';

@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.css'
})
export class PaymentComponent implements OnInit {
  constructor(private paymentService: PaymentService,
    private toastr: ToastrService, 
    private restaurantService: RestaurantService,
    private aroute: ActivatedRoute,
    private route: Router,
    private tableService: TableService,
    private cdr: ChangeDetectorRef){}
  reservationTable: reservation = {
    customerId: 0,
    reservationDateTime: new Date(),
    numberOfPeople: 0,
    status: false,
    tableId: 0
  }
  reservation: boolean = false;
  tableNumber: number = 0;
  ngOnInit(): void {
    this.paymentService.reserveTable$.subscribe(reserve => {
      this.reservation = reserve;
      this.cdr.detectChanges();
    })
    this.aroute.queryParams.subscribe(params => {
      this.handleOrderPayment(params);
      this.payAndBook(params);
    });
    this.restaurantService.fetchOrderDetails(this.paymentDetails.orderId).subscribe(
      {
        next: (res) => 
        {
          this.orderItems = res;
        }
      }
    )
  }
  orderId: number = 0;
  price: number = 0;
  orderItems: orders = {
    orderId: 0,
    customerId: 0,
    orderDate: new Date(),
    totalPrice: 0,
    menuId: 0,
    quantity: 0
  }
  paymentDetails: payment = {
    orderId: 0,
    amount: 0,
    paymentDateTime: new Date()
  }

  paymentForm = new FormGroup({
    cardNumber: new FormControl<any>('', [Validators.required, Validators.pattern(/^\d{16}$/)]),
    expiryDate: new FormControl<any>('', [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/?([0-9]{4}|[0-9]{2})$/)]),
    cvv: new FormControl<any>('', [Validators.required, Validators.pattern(/^\d{3,4}$/)])
  })

  payment()
  {
    const cardNumber = this.paymentForm.get('cardNumber')?.value;
    const expiryDate = this.paymentForm.get('expiryDate')?.value;
    const cvv = this.paymentForm.get('cvv')?.value;

    this.paymentService.updatePaymentStatus(this.paymentDetails).subscribe({
      next: (res : any) => 
      {
        if(res === true){
          this.toastr.success('Payment Recieved! Thank You For Ordering!');
          const orderItemsSerialized = this.orderItems ? JSON.stringify(this.orderItems) : '{}';
          this.route.navigate(['/invoice'], { queryParams: { order: orderItemsSerialized } });
        }
        else{
          this.toastr.error('Payment Failed');
        }
      },
      error: (err: any) => 
      {
        this.toastr.error(err);
      }
      
    })
  }

  handleOrderPayment(params: any): void
    {
      const idParam = params['id'];
      const priceParam = params['price'];
      if((idParam !== undefined && idParam !== null) && (priceParam !== undefined && priceParam !== null))
      {
        const orderId = +idParam;
        const price = +priceParam;
      if((!isNaN(orderId)) && (!isNaN(price)))
        this.paymentDetails.orderId = orderId;
        this.paymentDetails.amount = price;
      }
    }

  payAndBook(params: any): void
  {
      const customerIdParam = params['customerId'];
      const reservationDateTimeParam = params['reservationDateTime'];
      const numberOfPeopleParam = params['numberOfPeople'];
      const statusParam = params['status'];
      const tableIdParam = params['tableId']
      if ((customerIdParam !== undefined && customerIdParam !== null) &&
          (reservationDateTimeParam !== undefined && reservationDateTimeParam !== null) &&
          (numberOfPeopleParam !== undefined && numberOfPeopleParam !== null) &&
          (statusParam !== undefined && statusParam !== null)) {
        this.reservationTable = {
          customerId: +customerIdParam,
          reservationDateTime: new Date(reservationDateTimeParam),
          numberOfPeople: +numberOfPeopleParam,
          status: statusParam === 'true',
          tableId: +tableIdParam
        };
    }
  }
  reserveTable()
  {
    const cardNumber = this.paymentForm.get('cardNumber')?.value;
    const expiryDate = this.paymentForm.get('expiryDate')?.value;
    const cvv = this.paymentForm.get('cvv')?.value;
    const email = localStorage.getItem('customeremail') as string;
    this.paymentService.updatePaymentForTableBooking(this.reservationTable).subscribe({
      next: () => {
        this.paymentService.getTableNumber(this.reservationTable.numberOfPeople, email).subscribe({
          next: (res) => {
            this.tableNumber = res.tableId;
            this.tableService.updateStatus(this.tableNumber, "Booked").subscribe(
              {
                next: () =>
                {
                  this.toastr.success(`Table Booked! Assigned Table is Table Number ${this.tableNumber}. Kindly check Email for furthur details.`);
                  this.reservation = false;
                  this.paymentService.updateReservationStatus(false);
                  this.cdr.detectChanges();
                  this.route.navigate(['/menucategory']);
                }
              }
            )
          }
        })
      }
    })
  }
}
