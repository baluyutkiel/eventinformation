import { Component, OnInit, OnDestroy } from '@angular/core';
import { TickEvent } from '../../../models/events.model';
import { BehaviorSubject, switchMap, Subscription } from 'rxjs';
import { EventService } from '../../../core/services/event.service';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventListComponent implements OnInit, OnDestroy {
  events: TickEvent[] = [];
  selectedDays = 30;
  page = 1;
  pageSize = 10;
  total = 0;

  private $filter = new BehaviorSubject<{days: number, page: number}>({ days: 30, page: 1 });
  private sub!: Subscription;

  constructor(private eventService: EventService) {}

  ngOnInit() {
    this.sub = this.$filter
      .pipe(
        switchMap(({ days, page }) => this.eventService.getEvents(days, page, this.pageSize))
      )
      .subscribe({
        next: (response) => {
          this.events = response.events;
          this.total = response.total;
        },
        error: (err) => console.error('Error loading events:', err)
      });
  }

  onFilterDays(days: number) {
    this.selectedDays = days;
    this.page = 1;
    this.$filter.next({ days: this.selectedDays, page: this.page });
  }

  get pagedEvents(): TickEvent[] {
    const start = (this.page - 1) * this.pageSize;
    return this.events.slice(start, start + this.pageSize);
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.page++;
      this.$filter.next({ days: this.selectedDays, page: this.page });
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.$filter.next({ days: this.selectedDays, page: this.page });
    }
  }

  get totalPages(): number {
    const total = Math.ceil(this.total / this.pageSize);
    return total > 0 ? total : 1;
  }

  ngOnDestroy() {
    if (this.sub) this.sub.unsubscribe();
  }
}