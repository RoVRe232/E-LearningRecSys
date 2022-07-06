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
import { AuthGuard } from '../auth/services/auth.guard';
import { EditCourseComponent } from './admin/edit-course/edit-course.component';
import { CoursesOverviewComponent } from './admin/courses-overview/courses-overview.component';

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
    canActivate: [AuthGuard],
    component: OwnedCoursesPageComponent,
  },
  {
    path: 'course/details',
    component: CourseDetailsPageComponent,
  },
  {
    path: 'videos/watch',
    canActivate: [AuthGuard],
    component: VideoPageComponent,
  },
  {
    path: 'admin/home',
    canActivate: [AuthGuard],
    component: AdminHomeComponent,
  },
  {
    path: 'admin/add-course',
    canActivate: [AuthGuard],
    component: AddCourseComponent,
  },
  {
    path: 'admin/edit-course',
    canActivate: [AuthGuard],
    component: EditCourseComponent,
  },
  {
    path: 'admin/courses-overview',
    canActivate: [AuthGuard],
    component: CoursesOverviewComponent,
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
