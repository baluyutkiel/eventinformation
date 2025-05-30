import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './core/navbar/navbar.component';
import { EventListComponent } from './features/events/event-list/event-list.component';
import { SortableTableComponent } from './shared/components/sortable-table/sortable-table.component';
import { SalesSummaryComponent } from './features/sales-summary/sales-summary.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    EventListComponent,
    SortableTableComponent,
    SalesSummaryComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
