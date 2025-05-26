import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { SalesSummary } from '../../models/sales-summary';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private apiUrl = `${environment.apiBaseUrl}/tickets`;

  constructor(private http: HttpClient) {}

  getTop5ByAmount(): Observable<SalesSummary[]> {
    return this.http.get<SalesSummary[]>(`${this.apiUrl}/top5-by-amount-with-name`).pipe(
      catchError(error => {
        console.error('Error fetching top events by amount:', error);
        return throwError(() => error);
      })
    );
  }

  getTop5ByCount(): Observable<SalesSummary[]> {
    return this.http.get<SalesSummary[]>(`${this.apiUrl}/top5-by-count-with-name`).pipe(
      catchError(error => {
        console.error('Error fetching top events by count:', error);
        return throwError(() => error);
      })
    );
  }
}