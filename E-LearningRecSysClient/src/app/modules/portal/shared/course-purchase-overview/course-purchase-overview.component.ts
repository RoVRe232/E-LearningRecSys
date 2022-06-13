import { Component, Input } from '@angular/core';
import { CartService } from 'src/app/modules/shared/services/cart.service';
import { CourseModel } from '../../models/course.model';

@Component({
  selector: 'app-course-purchase-overview',
  templateUrl: './course-purchase-overview.component.html',
  styleUrls: ['./course-purchase-overview.component.scss'],
})
export class CoursePurchaseOverviewComponent {
  @Input() course!: CourseModel;

  constructor(private cartService: CartService) {}

  get formattedPrice() {
    if (this.course.price && !this.course.owned) {
      return `${this.course.price.amount} ${this.course.price.currency}`;
    }
    return 'Owned';
  }

  get owned() {
    return this.course.owned;
  }

  get sectionsCount() {
    return this.course.sections?.length;
  }

  get videosCount() {
    return this.course.sections?.reduce(
      (acc, curr) => (acc += curr.videos?.length || 0),
      0,
    );
  }

  onAddToCart() {
    this.cartService.addCourseToCardContent(this.course);
  }
}
