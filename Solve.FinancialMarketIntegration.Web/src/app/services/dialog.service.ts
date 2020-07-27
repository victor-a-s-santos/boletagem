import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs';
import { ConfirmationDialogComponent } from '../components/infrastructure/confirmation-dialog/confirmation-dialog.component';
import { RejectDialogComponent } from '../components/infrastructure/reject-dialog/reject-dialog.component';

@Injectable({
  providedIn: 'root'
})
export class DialogService {

  constructor(public dialog: MatDialog) { }

  confirm(title?: string, message?: string): Observable<boolean> {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '350px',
      height: '200px',
      data: {
        title,
        message
      }
    });

    return dialogRef.afterClosed();
  }

  reject(title?: string, message?: string): Observable<any> {
    const dialogRef = this.dialog.open(RejectDialogComponent, {
      width: '450px',
      height: '250px',
      data: {
        title,
        message
      }
    });

    return dialogRef.afterClosed();
  }

}
