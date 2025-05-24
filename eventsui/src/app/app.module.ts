import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { EventListComponent } from './features/events/event-list/event-list.component';
import { SortableTableComponent } from './shared/components/sortable-table/sortable-table.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EventListComponent,
    SortableTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
