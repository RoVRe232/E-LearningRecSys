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
import { AuthModule } from '../auth/_auth.module';
import { UserService } from '../auth/services/user.service';
import { MatStepperModule } from '@angular/material/stepper';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSelectModule } from '@angular/material/select';
import { NgxEditorModule } from 'ngx-editor';
import { VgCoreModule } from '@videogular/ngx-videogular/core';
import { VgControlsModule } from '@videogular/ngx-videogular/controls';
import { VgOverlayPlayModule } from '@videogular/ngx-videogular/overlay-play';
import { VgBufferingModule } from '@videogular/ngx-videogular/buffering';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { CartService } from './services/cart.service';

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
  MatStepperModule,
  MatExpansionModule,
  MatCheckboxModule,
  MatSelectModule,
];

@NgModule({
  declarations: [NavbarComponent, FooterComponent, SearchBarComponent],
  imports: [
    CommonModule,
    FlexLayoutModule,
    materialModules,
    NgxEditorModule,
    NgxSliderModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
  ],
  exports: [
    NavbarComponent,
    FooterComponent,
    SearchBarComponent,
    FlexLayoutModule,
    ReactiveFormsModule,
    materialModules,
    NgxEditorModule,
    NgxSliderModule,
    AuthModule,
    VgCoreModule,
    VgControlsModule,
    VgOverlayPlayModule,
    VgBufferingModule,
  ],
  providers: [UserService],
})
export class SharedModule {}
