import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/_shared.module';
import { HomeComponent } from './home/home.component';
import { ResultCardComponent } from './search-results/result-card/result-card.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { PortalRoutingModule } from './_portal-routing.module';

@NgModule({
  declarations: [HomeComponent, SearchResultsComponent, ResultCardComponent],
  imports: [PortalRoutingModule, CommonModule, SharedModule],
  providers: [],
  bootstrap: [HomeComponent, SearchResultsComponent, ResultCardComponent],
})
export class PortalModule {}
