import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthModule } from '../auth/_auth.module';
import { SharedModule } from '../shared/_shared.module';
import { AddCourseComponent } from './admin/add-course/add-course.component';
import { AddVideoComponent } from './admin/add-video/add-video.component';
import { AdminHomeComponent } from './admin/admin-home.component';
import { FileUploadComponent } from './admin/file-upload/file-upload.component';
import { VideoWrapperComponent } from './shared/video-wrapper/video-wrapper.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { ResultCardComponent } from './search-results/result-card/result-card.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { PortalRoutingModule } from './_portal-routing.module';
import { FormsModule } from '@angular/forms';
import { VideoPageComponent } from './video-page/video-page.component';
import { CartPageComponent } from './cart/cart-page.component';
import { CartSummaryComponent } from './cart/cart-summary/cart-summary.component';
import { CartOverviewComponent } from './cart/cart-overview/cart-overview.component';
import { CourseSectionsOverviewComponent } from './shared/course-sections-overview/course-sections-overview.component';

@NgModule({
  declarations: [
    HomeComponent,
    SearchResultsComponent,
    ResultCardComponent,
    ContactComponent,
    AddCourseComponent,
    AddVideoComponent,
    AdminHomeComponent,
    FileUploadComponent,
    VideoWrapperComponent,
    VideoPageComponent,
    CartPageComponent,
    CartSummaryComponent,
    CartOverviewComponent,
    CourseSectionsOverviewComponent,
  ],
  imports: [
    PortalRoutingModule,
    CommonModule,
    SharedModule,
    AuthModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [
    HomeComponent,
    SearchResultsComponent,
    ResultCardComponent,
    ContactComponent,
  ],
})
export class PortalModule {}
