import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { menu, menuCategory, orders } from '../models/model';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  constructor(private http: HttpClient) { }
  
  fetchCategory(): Observable<any>{
    return this.http.get(`${environment.baseUrl}/api/Category/GetAllItems`);
  }
  fetchItem(): Observable<any>{
    return this.http.get(`${environment.baseUrl}/api/Menu/GetListOfMenu`);
  }
  fetchMenuItems(categoryId: number): Observable<any>
  {
    return this.http.get(`${environment.baseUrl}/api/Menu/GetMenuByCategoryId?Id=${categoryId}`);
  }
  addOrder(order: orders): Observable<any>
  {
    const header = new HttpHeaders( {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('jwtoken')}`
  });
    return this.http.post<any>(`${environment.baseUrl}/api/Order/AddOrder`, order, { headers: header });
  }
  getMenuById(menuId: number): Observable<any>
  {
    return this.http.get<any>(`${environment.baseUrl}/api/Menu/GetMenuById?id=${menuId}`);
  }
  fetchOrderDetails(orderId: number): Observable<any>
  {
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('jwtoken')}`
    });
    return this.http.get<any>(`${environment.baseUrl}/api/Order/ListOfOrders?OrderId=${orderId}`, { headers: header });
  }

  fetchListOfOrders(customerId: number): Observable<any>
  {
    const header = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${localStorage.getItem('jwtoken')}`
    });
    return this.http.get<any>(`${environment.baseUrl}/api/Order/GetListOfOrdersForCustomer?CustomerId=${customerId}`, { headers: header});
  }

  fetchRestaurantDetails(): Observable<any>
  {
    return this.http.get<any>(`${environment.baseUrl}/api/About/GetListOfRestaurants`);
  }

  fetchMenuItem(name: string): Observable<any>
  {
    return this.http.get<any>(`${environment.baseUrl}/api/Menu/GetMenuByName?Name=${name}`);
  }
  addCategoryMenu(category: menuCategory): Observable<any>
  {
    return this.http.post<any>(`${environment.baseUrl}/api/Category/AddMenu`, category);
  }
  addImage(id: number, file: File): Observable<any>
  {
    const formData: FormData = new FormData();
    formData.append('formFile', file, file.name);
    return this.http.post<any>(`${environment.baseUrl}/api/Category/UpdateCategoryImage?Id=${id}`, formData);
  }
  addMenuItem(menu: menu): Observable<any>
  {
    return this.http.post<any>(`${environment.baseUrl}/api/Menu/AddMenu`, menu);
  }
  addItemImage(id: number, file: File): Observable<any>
  {
    const formData: FormData = new FormData();
    formData.append('formFile', file, file.name);
    return this.http.post<any>(`${environment.baseUrl}/api/Menu/UploadImage?Id=${id}`, formData);
  }
}
