import { Component } from '@angular/core';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html'
})
export class EventListComponent {
  events: Event[] = [];
  
  constructor() {}

  ngOnInit() {}

  ngOnDestroy() {}
}
