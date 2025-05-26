import { Component, OnInit, OnDestroy } from '@angular/core';
import { TickEvent } from '../../../models/events.model';
import { BehaviorSubject, switchMap, Subscription, combineLatest } from 'rxjs';
import { EventService } from '../../../core/services/event.service';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventListComponent implements OnInit, OnDestroy {
  events: TickEvent[] = [];
  selectedDays = 30;
  private $filterDays = new BehaviorSubject<number>(30);
  private $page = new BehaviorSubject<number>(1);
  private sub!: Subscription;

  page = 1;
  pageSize = 10;
  total = 0;

  constructor(private eventService: EventService) {}

  ngOnInit() {
    this.sub = combineLatest([this.$filterDays, this.$page])
      .pipe(
        switchMap(([days, page]) => this.eventService.getEvents(days, page, this.pageSize))
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
    this.$filterDays.next(this.selectedDays);
    this.$page.next(this.page);
  }

  get pagedEvents(): TickEvent[] {
    const start = (this.page - 1) * this.pageSize;
    return this.events.slice(start, start + this.pageSize);
  }

  nextPage() {
    if (this.page < this.totalPages) {
      this.page++;
      this.$page.next(this.page);
    }
  }

  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.$page.next(this.page);
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