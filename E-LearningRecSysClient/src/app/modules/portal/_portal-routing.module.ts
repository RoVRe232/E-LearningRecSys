import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddCourseComponent } from './admin/add-course/add-course.component';
import { AdminHomeComponent } from './admin/admin-home.component';
import { VideoWrapperComponent } from './shared/video-wrapper/video-wrapper.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { VideoPageComponent } from './video-page/video-page.component';
import { CartPageComponent } from './cart/cart-page.component';
import { OwnedCoursesPageComponent } from './owned-courses-page/owned-courses-page.component';
import { CourseDetailsComponent } from './shared/course-details/course-details.component';
import { CourseDetailsPageComponent } from './course/course-details-page/course-details-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'contact-us',
    component: ContactComponent,
  },
  {
    path: 'search-results',
    component: SearchResultsComponent,
  },
  {
    path: 'courses/owned-courses',
    component: OwnedCoursesPageComponent,
  },
  {
    path: 'course/details',
    component: CourseDetailsPageComponent,
  },
  {
    path: 'videos/watch',
    component: VideoPageComponent,
  },
  {
    path: 'admin/home',
    component: AdminHomeComponent,
  },
  {
    path: 'admin/add-course',
    component: AddCourseComponent,
  },
  {
    path: 'cart',
    component: CartPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PortalRoutingModule {}
