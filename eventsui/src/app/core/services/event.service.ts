import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { TickEvent } from '../../models/events.model';
import { environment } from '../../../environments/environment';

interface PaginatedEventsResponse {
  total: number;
  page: number;
  pageSize: number;
  events: TickEvent[];
}

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = `${environment.apiBaseUrl}/events`;

  constructor(private http: HttpClient) {}

getEvents(days: number, page: number = 1, pageSize: number = 10): Observable<PaginatedEventsResponse> {
  return this.http
    .get<PaginatedEventsResponse>(`${this.apiUrl}/${days}?page=${page}&pageSize=${pageSize}`)
    .pipe(
      catchError(error => {
        console.error('Error fetching events:', error);
        return throwError(() => error);
      })
    );
}
}