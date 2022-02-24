import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
      import('./modules/portal/_portal.module').then((m) => m.PortalModule),
  },
  {
    path: 'accounts',
    loadChildren: () =>
      import('./modules/accounts/_accounts.module').then(
        (m) => m.AccountsModule,
      ),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
