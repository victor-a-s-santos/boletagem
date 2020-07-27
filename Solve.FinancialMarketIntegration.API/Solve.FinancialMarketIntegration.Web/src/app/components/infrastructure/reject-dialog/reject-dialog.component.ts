import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-reject-dialog',
  templateUrl: './reject-dialog.component.html',
  styleUrls: ['./reject-dialog.component.scss']
})
export class RejectDialogComponent implements OnInit {

  form: FormGroup;

  title: string;
  message: string;

  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<RejectDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data: any) {
      this.title = data.title;
      this.message = data.message;
  }

  ngOnInit() {
    this.form = this.fb.group({
      justification: ['', [
        Validators.required,
        Validators.minLength(4),
        Validators.pattern(/^[^\s][0-9a-zA-Z\u00C0-\u00D6\u00D9-\u00DC\u00E0-\u00fc\s.,]{2,}$/),
        this.noWhitespaceValidator
      ]]
    });
  }

  noWhitespaceValidator(control: FormControl) {
    const isWhitespace = (control.value || '').trim().length === 0;
    const isValid = !isWhitespace;
    return isValid ? null : { 'whitespace': true };
  }

  confirmRejection(): void {
    if (this.form.valid) {
      this.dialogRef.close(this.form.value);
    }
  }

  close(): void {
    this.dialogRef.close();
  }
}
