import { Component, OnInit } from '@angular/core';
import { TickEvent } from '../../models/events.model';

interface Event {
  id: string;
  name: string;
  startDate: Date;
  endDate: Date;
  sales: number;
}

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html'
})
export class SalesSummaryComponent implements OnInit {
  events: any[] = [
    { id: 'e1', name: 'Concert', startDate: new Date('2025-06-01'), endDate: new Date('2025-06-02'), sales: 1200 },
    { id: 'e2', name: 'Festival', startDate: new Date('2025-07-10'), endDate: new Date('2025-07-12'), sales: 2500 },
    { id: 'e3', name: 'Conference', startDate: new Date('2025-05-15'), endDate: new Date('2025-05-16'), sales: 800 },
    { id: 'e4', name: 'Meetup', startDate: new Date('2025-08-05'), endDate: new Date('2025-08-05'), sales: 600 },
    { id: 'e5', name: 'Workshop', startDate: new Date('2025-09-01'), endDate: new Date('2025-09-01'), sales: 1500 },
    { id: 'e6', name: 'Play', startDate: new Date('2025-04-20'), endDate: new Date('2025-04-22'), sales: 900 }
  ];

  topEvents: Event[] = [];

  ngOnInit(): void {
    // Sort descending by sales and get top 5
    this.topEvents = this.events
      .sort((a, b) => b.sales - a.sales)
      .slice(0, 5);
  }
}