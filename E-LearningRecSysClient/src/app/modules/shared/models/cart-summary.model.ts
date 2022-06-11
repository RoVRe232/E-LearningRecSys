import { CartSummaryItemModel } from './cart-summary-item.model';

export interface CartSummaryModel {
  cartSummaryItems: CartSummaryItemModel[];
  total: number;
  currency: string;
}
