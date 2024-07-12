import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { payment, reservation } from '../models/model';
import { environment } from '../../environments/environment.development';
import { Observable, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  constructor(private http: HttpClient, private route: Router) { }
  private reserveTable = new BehaviorSubject<boolean>(false);
  reserveTable$ = this.reserveTable.asObservable();
  updatePaymentStatus(paymentDetails: payment): Observable<any>
  {
    return this.http.post<any>(`${environment.baseUrl}/api/Payment/UpdatePaymentStatus`, paymentDetails);
  }

  updatePaymentForTableBooking(reservation: reservation): Observable<any>
  {
    this.route.navigate(['/booktable']);
    return this.http.post<any>(`${environment.baseUrl}/api/Reservation/AddReservation`, reservation);
  }

  getTableNumber(seatingCapacity: number, email: string): Observable<any>
  {
    return this.http.get<any>(`${environment.baseUrl}/api/Reservation/GetTableNumber?seatingCapacity=${seatingCapacity}&email=${email}`);
  }

  updateReservationStatus(status: boolean) {
    this.reserveTable.next(status);
  }
}
