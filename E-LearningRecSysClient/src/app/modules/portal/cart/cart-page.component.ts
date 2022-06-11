import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Subject, takeUntil } from 'rxjs';
import { CartSummaryModel } from '../../shared/models/cart-summary.model';
import { CourseCardModel } from '../../shared/models/course-card.model';
import { CartService } from '../../shared/services/cart.service';

@Component({
  selector: 'app-cart-page',
  templateUrl: './cart-page.component.html',
  styleUrls: ['./cart-page.component.scss'],
})
export class CartPageComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  public cartContent: CourseCardModel[] = [];
  public cartSummary: CartSummaryModel = {
    cartSummaryItems: [],
    total: 0,
    currency: 'USD',
  };

  constructor(private fb: FormBuilder, private cartService: CartService) {}

  ngOnInit(): void {
    this.cartService.cartContentValue
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((e) => (this.cartContent = e));

    this.cartService.cartSummary
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((e) => (this.cartSummary = e));
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }
}
