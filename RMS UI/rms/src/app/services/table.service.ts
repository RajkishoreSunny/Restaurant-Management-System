import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { table } from '../models/model';

@Injectable({
  providedIn: 'root'
})
export class TableService {

  constructor(private http: HttpClient) { }
  getAllTables(): Observable<any>
  {
    return this.http.get(`${environment.baseUrl}/api/Table/ListOfTables`);
  }
  
  showAvailableTables(): Observable<any>
  {
    return this.http.get(`${environment.baseUrl}/api/Table/AvailableTables`);
  }

  updateStatus(id: number, status: string):Observable<any>
  {
    return this.http.post<any>(`${environment.baseUrl}/api/Table/UpdateStatus/${id}/${status}`, null);
  }
  updateTable(id: number, table: table): Observable<any>
  {
    return this.http.post<any>(`${environment.baseUrl}/api/Table/UpdateTable?id=${id}`, table);
  }
  deleteTable(id: number): Observable<any>
  {
    return this.http.delete<any>(`${environment.baseUrl}/api/Table/DeleteTable/${id}`);
  }
}
