import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AccountsModule } from './modules/accounts/_accounts.module';
import { SharedModule } from './modules/shared/_shared.module';
import { PortalModule } from './modules/portal/_portal.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BackapiOutgoingInterceptor } from './modules/shared/interceptors/backapi-outgoing.interceptor';

@NgModule({
  declarations: [AppComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    SharedModule,
    AccountsModule,
    PortalModule,
  ],
  providers: [
    [
      {
        provide: HTTP_INTERCEPTORS,
        useClass: BackapiOutgoingInterceptor,
        multi: true,
      },
    ],
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
