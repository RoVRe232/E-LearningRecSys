import { HttpClient } from '@angular/common/http';
import { Component, ElementRef, OnDestroy } from '@angular/core';
import {
  AbstractControl,
  FormArray,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Editor } from 'ngx-editor';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-add-course',
  templateUrl: './add-course.component.html',
  styleUrls: ['./add-course.component.scss'],
})
export class AddCourseComponent implements OnDestroy {
  isSubmitted: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  courseDataForm!: ElementRef;

  editor: Editor;
  courseDescription = '';

  sectionIndex = new FormControl(0);
  videoIndex = new FormControl(0);

  constructor(private fb: FormBuilder, private httpClient: HttpClient) {
    this.editor = new Editor();
  }

  ngOnDestroy(): void {
    this.editor.destroy();
  }

  courseData = this.fb.group({
    name: ['', [Validators.required]],
    smallDescription: ['', [Validators.required]],
    largeDescription: [''],
    thumbnailImage: ['', [Validators.required]],
    price: this.fb.group({
      amount: [0, [Validators.required, Validators.min(1)]],
      currency: ['USD', [Validators.required]],
    }),
    sections: this.fb.array([]),
  });

  get sections() {
    return this.courseData.controls['sections'] as FormArray;
  }

  get price() {
    return this.courseData.controls['price'] as FormGroup;
  }

  get name() {
    return this.courseData.controls['name'] as FormControl;
  }

  get smallDescription() {
    return this.courseData.controls['smallDescription'] as FormControl;
  }

  get amount() {
    return this.price.controls['amount'] as FormControl;
  }

  get currency() {
    return this.price.controls['currency'] as FormControl;
  }

  get thumbnailImage() {
    return this.courseData.controls['thumbnailImage'] as FormControl;
  }

  get largeDescription() {
    return this.courseData.controls['largeDescription'] as FormControl;
  }

  abstractControlToFormGroup(abstractControl: AbstractControl) {
    return abstractControl as FormGroup;
  }

  abstractControlToFormArray(abstractControl: AbstractControl | null) {
    return abstractControl as FormArray;
  }

  addSection() {
    const sectionForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      thumbnailImage: ['', Validators.required],
      videos: this.fb.array([]),
    });

    this.sections.push(sectionForm);
  }

  addSectionAtIndex() {
    const sectionForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      thumbnailImage: ['', Validators.required],
    });

    this.sections.insert(this.sectionIndex.value, sectionForm);
  }

  deleteSection(sectionIndex: number) {
    this.sections.removeAt(sectionIndex);
  }

  addVideo(sectionIndex: number) {
    const videoForm = this.getNewVideoFormGroup();
    (this.getSectionAtIndex(sectionIndex).get('videos') as FormArray).push(
      videoForm,
    );
  }

  addVideoAtIndex(sectionIndex: number) {
    const videoForm = this.getNewVideoFormGroup();
    (this.getSectionAtIndex(sectionIndex).get('videos') as FormArray).insert(
      this.videoIndex.value,
      videoForm,
    );
  }

  deleteVideo(sectionIndex: number, videoIndex: number) {
    const videos = this.getSectionAtIndex(sectionIndex).get(
      'videos',
    ) as FormArray;
    videos.removeAt(videoIndex);
  }

  onSubmit() {
    this.largeDescription.setValue(this.courseDescription);
    const formData = new FormData();
    formData.append('name', this.name.value);

    this.httpClient
      .post('https://localhost:5001/api/courses', this.courseData.value)
      .subscribe((e) => console.log(e));

    this.isSubmitted.next(true);
  }

  private getSectionAtIndex(sectionIndex: number) {
    return this.sections.at(sectionIndex);
  }

  private getNewVideoFormGroup() {
    const internalId = this.generateGuid();
    return this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      source: this.fb.group({
        internalId,
        type: [''],
        location: [''],
        videoContent: [null],
      }),
      internalId,
      keywords: ['', Validators.required],
      thumbnail: ['', Validators.required], // TODO add upload for images
      hidden: [false],
      hiddenInSearches: [false],
      creationDate: [new Date(Date.now())],
      language: ['en'],
      duration: [0],
      transcription: [''],
    });
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
