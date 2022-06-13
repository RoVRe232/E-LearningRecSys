import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  constructor(private snackBar: MatSnackBar) {}

  public showSuccessNotification(message: string, action?: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['mat-toolbar', 'mat-success'],
      duration: 5 * 1000,
    });
  }

  public showFailureNotification(message: string, action?: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['mat-toolbar', 'mat-warn'],
      duration: 5 * 1000,
    });
  }

  public showInfoNotification(message: string, action?: string) {
    this.snackBar.open(message, action, {
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
      panelClass: ['mat-toolbar', 'mat-info'],
      duration: 5 * 1000,
    });
  }
}
