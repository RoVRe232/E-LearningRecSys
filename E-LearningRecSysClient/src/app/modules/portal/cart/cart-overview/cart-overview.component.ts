import { Component, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CourseCardModel } from 'src/app/modules/shared/models/course-card.model';
import { CartService } from 'src/app/modules/shared/services/cart.service';

@Component({
  selector: 'app-cart-overview',
  templateUrl: './cart-overview.component.html',
  styleUrls: ['./cart-overview.component.scss'],
})
export class CartOverviewComponent {
  @Input() cartContent: CourseCardModel[] = [];
  constructor(private fb: FormBuilder, private cartService: CartService) {}
}
