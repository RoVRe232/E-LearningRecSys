import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-video-wrapper',
  templateUrl: './video-wrapper.component.html',
  styleUrls: ['./video-wrapper.component.scss'],
})
export class VideoWrapperComponent implements OnInit {
  @Input() videoId!: string;
  @ViewChild('videoRef', { static: true }) videoRef!: ElementRef;
  isLoaded = false;
  videoContent = '';

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.httpClient
      .get(
        `https://localhost:6001/api/videos/get-source-content?id=${this.videoId}`,
        { responseType: 'text' },
      )
      .subscribe((e) => {
        this.videoContent = e;
        this.isLoaded = true;
        this.videoRef.nativeElement.play();
      });
  }
}
