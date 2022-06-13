import { Injectable } from '@angular/core';
import { map, Subject, take } from 'rxjs';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { BackApiHttpRequest } from '../models/back-api-http-request.model';
import { CartSummaryItemModel } from '../models/cart-summary-item.model';
import { CartSummaryModel } from '../models/cart-summary.model';
import { CourseCardModel } from '../models/course-card.model';
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class CartService {
  private cartContent: BehaviorSubject<CourseCardModel[]>;

  constructor(private httpService: HttpService) {
    if (localStorage.getItem('cartContent')) {
      const cartContent: CourseCardModel[] = JSON.parse(
        // eslint-disable-next-line @typescript-eslint/no-non-null-assertion
        localStorage.getItem('cartContent')!,
      );
      this.cartContent = new BehaviorSubject(cartContent);
    } else {
      this.cartContent = new BehaviorSubject(new Array<CourseCardModel>());
    }
  }

  get cartSize() {
    return this.cartContent.asObservable().pipe(map((e) => e.length));
  }

  get cartContentValue() {
    return this.cartContent.asObservable();
  }

  get cartSummary() {
    return this.cartContent.asObservable().pipe(
      map((cartContent) => {
        const total = cartContent.reduce((acc, course) => {
          return (acc += course.price ? course.price.amount : 0);
        }, 0);
        const mappedCartContent: Array<CartSummaryItemModel> = cartContent.map(
          (e) => {
            return {
              name: e.name,
              amount: e.price?.amount || 0,
              currency: e.price?.currency || 'USD',
            } as CartSummaryItemModel;
          },
        );
        return {
          cartSummaryItems: mappedCartContent,
          total,
          currency: 'USD',
        } as CartSummaryModel;
      }),
    );
  }

  public addCourseToCardContent(course: CourseCardModel | undefined) {
    if (course) {
      const cartState = [...this.cartContent.value, course];
      this.cartContent.next(cartState);
      this.storeCartContentToLocalStorage();
    }
  }

  public removeCourseFromCardContent(courseID: string) {
    let cartState = [...this.cartContent.value];
    cartState = cartState.filter((e) => e.courseID !== courseID);
    this.cartContent.next(cartState);
    this.storeCartContentToLocalStorage();
  }

  public clearCartContent() {
    this.cartContent.next(new Array<CourseCardModel>());
    this.storeCartContentToLocalStorage();
  }

  public storeCartContentToLocalStorage() {
    const cartState = [...this.cartContent.value];
    localStorage.setItem('cartContent', JSON.stringify(cartState));
  }

  public sendOrderToApi() {
    const orderRequest = new BackApiHttpRequest(
      'api/courses/new-order',
      {},
      {
        courses: this.cartContent.value,
        created: new Date(Date.now()),
      },
    );
    this.httpService
      .post(orderRequest)
      .pipe(take(1))
      .subscribe((order) => console.log(order));
  }
}
