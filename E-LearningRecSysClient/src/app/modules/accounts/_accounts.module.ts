import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthModule } from '../auth/_auth.module';
import { SharedModule } from '../shared/_shared.module';
import { LoginComponent } from './login/login.component';
import { AccountsRoutingModule } from './_accounts-routing.module';

@NgModule({
  declarations: [LoginComponent],
  imports: [AccountsRoutingModule, CommonModule, SharedModule, AuthModule],
  providers: [],
})
export class AccountsModule {}
