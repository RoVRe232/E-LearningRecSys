import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthModule } from '../auth/_auth.module';
import { SharedModule } from '../shared/_shared.module';
import { LoginComponent } from './login/login.component';
import { MyAccountComponent } from './my-account/my-account.component';
import { SignupComponent } from './signup/signup.component';
import { AccountsRoutingModule } from './_accounts-routing.module';

@NgModule({
  declarations: [LoginComponent, SignupComponent, MyAccountComponent],
  imports: [AccountsRoutingModule, CommonModule, SharedModule, AuthModule],
  providers: [],
})
export class AccountsModule {}
