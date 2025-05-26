import { Component, OnInit } from '@angular/core';
import { SalesSummary } from '../../models/sales-summary';
import { SalesService } from '../../core/services/sales.service';
import { BehaviorSubject, Subscription } from 'rxjs';

@Component({
  selector: 'app-sales-summary',
  templateUrl: './sales-summary.component.html',
})
export class SalesSummaryComponent implements OnInit {
  currentFilter: 'amount' | 'count' = 'amount';
  topEvents: SalesSummary[] = [];
  error: string | null = null;

  private byAmount$ = new BehaviorSubject<void>(undefined);
  private byCount$ = new BehaviorSubject<void>(undefined);
  private subs = new Subscription();

  constructor(private salesService: SalesService) {}

  ngOnInit(): void {
    this.subs.add(
      this.byAmount$.subscribe(() => {
        this.error = null;
        this.salesService.getTop5ByAmount().subscribe({
          next: events => this.topEvents = events,
          error: () => this.error = 'Failed to load top events by amount.'
        });
      })
    );

    this.subs.add(
      this.byCount$.subscribe(() => {
        this.error = null;
        this.salesService.getTop5ByCount().subscribe({
          next: events => this.topEvents = events,
          error: () => this.error = 'Failed to load top events by count.'
        });
      })
    );

    this.byAmount$.next();
  }

  onFilterChange(filter: 'amount' | 'count'): void {
    this.currentFilter = filter;
    if (filter === 'amount') {
      this.byAmount$.next();
    } else {
      this.byCount$.next();
    }
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }
}