import { Component, ElementRef, HostListener, Input } from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: FileUploadComponent,
      multi: true,
    },
  ],
})
export class FileUploadComponent {
  @Input() fileUploadControl;
  @Input() progress;

  private file: File | null = null;

  @HostListener('change', ['$event.target.files']) emitFiles(event: FileList) {
    const file = event && event.item(0);
    this.file = file;
    console.log(this.file);
  }

  constructor(private host: ElementRef<HTMLInputElement>) {
    this.fileUploadControl = new FormControl();
    this.progress = 0;
  }

  writeValue(value: null) {
    this.host.nativeElement.value = '';
    this.file = null;
  }
}
