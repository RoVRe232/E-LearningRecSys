import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { UserService } from './services/user.service';

@NgModule({
  declarations: [],
  imports: [CommonModule],
  providers: [UserService],
  bootstrap: [],
})
export class AuthModule {}
