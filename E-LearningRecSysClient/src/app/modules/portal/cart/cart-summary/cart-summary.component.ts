import { Component, Input } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CartSummaryItemModel } from 'src/app/modules/shared/models/cart-summary-item.model';
import { CartSummaryModel } from 'src/app/modules/shared/models/cart-summary.model';
import { CartService } from 'src/app/modules/shared/services/cart.service';

@Component({
  selector: 'app-cart-summary',
  templateUrl: './cart-summary.component.html',
  styleUrls: ['./cart-summary.component.scss'],
})
export class CartSummaryComponent {
  @Input() cartSummary!: CartSummaryModel;

  constructor(private cartService: CartService) {}

  formattedPrice(cartItem: CartSummaryItemModel) {
    if (cartItem) {
      return `${cartItem.amount} ${cartItem.currency}`;
    }
    return 'Owned';
  }

  formattedTotal() {
    if (this.cartSummary) {
      return `${this.cartSummary.total} ${this.cartSummary.currency}`;
    }
    return '0 USD';
  }

  onCheckoutRedirect() {
    this.cartService.sendOrderToApi();
  }
}
