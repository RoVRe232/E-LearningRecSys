import { NgModule } from '@angular/core';
import { FooterComponent } from './footer/footer.component';
import { NavbarComponent } from './navbar/navbar.component';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCommonModule } from '@angular/material/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SearchBarComponent } from './search/search-bar.component';
import { MatMenuModule } from '@angular/material/menu';
import { HttpService } from './services/http.service';
import { SearchService } from './services/search.service';
import { RouterModule } from '@angular/router';
import { MatChipsModule } from '@angular/material/chips';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatPaginatorModule } from '@angular/material/paginator';

const materialModules = [
  MatButtonModule,
  MatCardModule,
  MatCommonModule,
  MatAutocompleteModule,
  MatIconModule,
  MatOptionModule,
  MatFormFieldModule,
  MatInputModule,
  MatMenuModule,
  MatChipsModule,
  MatSidenavModule,
  MatPaginatorModule,
];

@NgModule({
  declarations: [NavbarComponent, FooterComponent, SearchBarComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    materialModules,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
  ],
  exports: [NavbarComponent, FooterComponent, SearchBarComponent],
  providers: [HttpService, SearchService],
})
export class SharedModule {}
