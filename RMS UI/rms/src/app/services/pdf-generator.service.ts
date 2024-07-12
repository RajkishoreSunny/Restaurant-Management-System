import { Injectable } from '@angular/core';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { orders } from '../models/model';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class PdfGeneratorService {

  constructor(private http: HttpClient) { }
  public generatePdf(element: HTMLElement, fileName: string): void
  {
    html2canvas(element).then((canvas) => {
      const imgData = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');
      const imgProps = pdf.getImageProperties(imgData);
      const pdfWidth = pdf.internal.pageSize.getWidth();
      const pdfHeight = (imgProps.height * pdfWidth) / imgProps.width;
      pdf.addImage(imgData, 'PNG', 0, 0, pdfWidth, pdfHeight);
      pdf.save(`${fileName}.pdf`);
    });
  }
  sendInvoiceEmail(order: any, file: Blob): Observable<any> {
    const formData = new FormData();
    formData.append('formFile', file);
    formData.append('orderId', order.orderId.toString());
    formData.append('customerId', order.customerId.toString());
    const orderDate = new Date(order.orderDate);
    if (!isNaN(orderDate.getTime())) {
        formData.append('orderDate', orderDate.toISOString());
    }
    formData.append('totalPrice', order.totalPrice.toString());
    formData.append('menuId', order.menuId.toString());
    formData.append('quantity', order.quantity.toString());

    return this.http.post<any>(`${environment.baseUrl}/api/Order/SendOrderInvoiceEmail`, formData);
  }
}
