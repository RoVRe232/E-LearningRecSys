import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthModule } from '../auth/_auth.module';
import { SharedModule } from '../shared/_shared.module';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { ResultCardComponent } from './search-results/result-card/result-card.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { PortalRoutingModule } from './_portal-routing.module';

@NgModule({
  declarations: [
    HomeComponent,
    SearchResultsComponent,
    ResultCardComponent,
    ContactComponent,
  ],
  imports: [PortalRoutingModule, CommonModule, SharedModule, AuthModule],
  providers: [],
  bootstrap: [
    HomeComponent,
    SearchResultsComponent,
    ResultCardComponent,
    ContactComponent,
  ],
})
export class PortalModule {}
