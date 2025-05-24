import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-sortable-table',
  templateUrl: './sortable-table.component.html'
})
export class SortableTableComponent {
  @Input() events: any[] = [];

  constructor() {}

  ngOnInit() {}

  ngOnDestroy() {}

  sortColumn: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  sortBy(column: string) {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.events.sort((a, b) => {
      const aVal = a[column];
      const bVal = b[column];

      if (aVal < bVal) return this.sortDirection === 'asc' ? -1 : 1;
      if (aVal > bVal) return this.sortDirection === 'asc' ? 1 : -1;
      return 0;
    });
  }
}