import { Component } from '@angular/core';
import { TickEvent } from '../../../models/events.model';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventListComponent {
  events: TickEvent[] = [
    { id: 'e1', name: 'Concert', startDate: new Date('2025-06-01'), endDate: new Date('2025-06-02')},
    { id: 'e2', name: 'Festival', startDate: new Date('2025-07-10'), endDate: new Date('2025-07-12')},
    { id: 'e3', name: 'Conference', startDate: new Date('2025-05-15'), endDate: new Date('2025-05-16')},
    { id: 'e4', name: 'Meetup', startDate: new Date('2025-08-05'), endDate: new Date('2025-08-05')},
    { id: 'e5', name: 'Workshop', startDate: new Date('2025-09-01'), endDate: new Date('2025-09-01')},
    { id: 'e6', name: 'Play', startDate: new Date('2025-04-20'), endDate: new Date('2025-04-22')}
  ];

  
  constructor() {}

  ngOnInit() {}

  ngOnDestroy() {}
}
