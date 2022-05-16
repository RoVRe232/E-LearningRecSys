import { HttpClient } from '@angular/common/http';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Editor, Validators } from 'ngx-editor';
import { BehaviorSubject, concatMap, filter, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-add-video',
  templateUrl: './add-video.component.html',
  styleUrls: ['./add-video.component.scss'],
})
export class AddVideoComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();

  @Input() isSubmitted!: BehaviorSubject<boolean>;
  @Input() videoForm: FormGroup;
  editor: Editor;
  videoDescription = '';

  constructor(private fb: FormBuilder, private httpClient: HttpClient) {
    this.videoForm = this.fb.group({
      title: ['', Validators.required],
      description: [this.videoDescription],
      videoSource: this.fb.group({
        internalId: [this.generateGuid()],
        type: [''],
        location: [''],
        videoContent: [null],
      }),
      keywords: ['', Validators.required],
      thumbnail: ['', Validators.required], // TODO add upload for images
      //add slides
      hidden: [false],
      hiddenInSearches: [false],
      creationDate: [new Date(Date.now())],
      language: ['en'],
      duration: [0],
      transcription: [''],
    });
    this.editor = new Editor();
  }

  ngOnInit(): void {
    this.isSubmitted
      .pipe(
        takeUntil(this.ngUnsubscribe),
        filter((e) => e === true),
      )
      .subscribe(() => {
        const formData = new FormData();
        formData.append('internalId', this.internalId.value);
        formData.append('type', this.type.value);
        formData.append('location', this.location.value);
        formData.append(
          'videoContent',
          this.videoContent.value,
          this.location.value,
        );
        this.httpClient
          .post<any>(
            'https://localhost:5001/api/videos/add-source-content',
            formData,
          )
          .subscribe((e) => console.log(e));
      });
  }

  get videoSource() {
    return this.videoForm.get('source') as FormGroup;
  }

  get internalId() {
    return this.videoSource.get('internalId') as FormControl;
  }

  get type() {
    return this.videoSource.get('type') as FormControl;
  }

  get location() {
    return this.videoSource.get('location') as FormControl;
  }

  get videoContent() {
    return this.videoSource.get('videoContent') as FormControl;
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();

    this.editor.destroy();
  }

  uploadDocument(event: any) {
    // var fr = new FileReader();
    // fr.onloadend = function(){
    //   //fr.result will be an ArrayBuffer so create an Int8Array view
    //   var data = new Int8Array(fr.result);
    //   //use data as you wish.
    // };
    // fr.readFileAsArrayBuffer(file.files[0]);

    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      reader.onloadend = () => {
        this.videoContent.setValue(event.target.files[0]);
      };
    }
  }

  private generateGuid(): string {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(
      /[xy]/g,
      function (c) {
        const r = (Math.random() * 16) | 0,
          v = c == 'x' ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      },
    );
  }
}
