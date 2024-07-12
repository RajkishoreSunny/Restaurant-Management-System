import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { menu, orders } from '../models/model';
import { LoginService } from '../services/login.service';
import { RestaurantService } from '../services/restaurant.service';
import { PdfGeneratorService } from '../services/pdf-generator.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import html2canvas from 'html2canvas';
import jsPDF from 'jspdf';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrl: './invoice.component.css'
})
export class InvoiceComponent implements OnInit {
  @ViewChild('invoiceContent', {static: false}) invoiceContent!: ElementRef;
  @ViewChild('downloadButton', {static: false}) downloadButton!: ElementRef;

  invoiceForm: FormGroup;
  attachment: File | null = null;
  constructor(private activatedRoute: ActivatedRoute, 
    private loginService: LoginService, 
    private restaurantservice: RestaurantService,
    private pdfGeneratorService: PdfGeneratorService,
    private fb: FormBuilder,
    private toastr: ToastrService)
    {
      this.invoiceForm = this.fb.group({});
    }
  orderItems: orders = {
    orderId: 0,
    customerId: 0,
    orderDate: new Date(),
    totalPrice: 0,
    menuId: 0,
    quantity: 0
  }
  item: menu = {
    menuId: 0,
    description: '',
    name: '',
    categoryId: 0,
    itemImg: '',
    price: 0,
    rating: 0

  };
  currDate = this.formatDate(new Date());
  email = localStorage.getItem('customeremail');
  custId = Number(localStorage.getItem('customerId'));
  menuId: number = 0;
  customerName: string = '';
  getDetails(): void{
    this.loginService.getCustomerDetails(this.custId).subscribe(
      {
        next: (res) => {
          this.customerName = res.customerName;
        }
      }
    )
  }
  ngOnInit(): void {
    this.getDetails();
    this.activatedRoute.queryParams.subscribe(params => 
      {
        if (params['order']) {
          try {
            const parsedOrder = JSON.parse(params['order']);
            this.orderItems = parsedOrder;

            if (Array.isArray(parsedOrder) && parsedOrder.length > 0) {
              this.orderItems = parsedOrder[0];
            }

            if(this.orderItems && this.orderItems.customerId)
            {
              this.menuId = this.orderItems.menuId;
            }
          } catch (error) {
            console.error('Error parsing order items:', error);
            this.orderItems = this.getDefaultOrderItems();
            if(this.orderItems && this.orderItems.menuId)
            {
              this.menuId = this.orderItems.menuId;
            }
            else
            {
              console.error('error');
            }
          }
        } else {
          this.orderItems = this.getDefaultOrderItems();
          this.menuId = this.orderItems.menuId;
        }
        this.getMenuItem(this.menuId);
      }
    )
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${day}/${month}/${year}`;
  }

  getMenuItem(id: number): void
  {
    this.restaurantservice.getMenuById(id).subscribe(
      {
        next: (res) => {
          this.item = res;
        }
      }
    )
  }

  getDefaultOrderItems(): orders {
    return {
      customerId: 0,
      orderDate: new Date(),
      totalPrice: 0,
      menuId: 0,
      quantity: 0
    };
  }
  downloadPdf()
  {
    this.downloadButton.nativeElement.style.display = 'none';
    setTimeout(() => {
      if (this.invoiceContent) {
        this.pdfGeneratorService.generatePdf(this.invoiceContent.nativeElement, 'invoice');
        this.toastr.success('Downloaded Successfully!');
      } else {
        console.error('Invoice content not found');
      }
    }, 0);
  }

  convertToPdf(): void
  {
    const data = this.invoiceContent.nativeElement;
    html2canvas(data).then(c => 
      {
        const imgWidth = 208;
        const imgHeight = c.height * imgWidth / c.width;
        const imgData = c.toDataURL('image/png');
        const pdf = new jsPDF('p', 'mm', 'a4');
        pdf.addImage(imgData, 'PNG', 0, 0, imgWidth, imgHeight);
        const pdfFile = pdf.output('blob');

        this.sendInvoiceEmail(pdfFile);
      }
    )
  }
  sendInvoiceEmail(file: Blob): void {
      this.pdfGeneratorService.sendInvoiceEmail(this.orderItems, file).subscribe(
        {
          next: (res) => 
            {
              if(res)
                {
                  this.toastr.success('Invoice email sent successfully. Please check your registered email.');
                }
              else
              {
                this.toastr.error('Failed to send invoice email')
              }
            }
        }
      );
  }
}
