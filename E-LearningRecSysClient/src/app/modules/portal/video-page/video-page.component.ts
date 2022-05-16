import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-video-page',
  templateUrl: './video-page.component.html',
  styleUrls: ['./video-page.component.scss'],
})
export class VideoPageComponent implements OnDestroy {
  private ngUnsubscribe = new Subject<boolean>();
  public videoId: string;

  constructor(private route: ActivatedRoute) {
    this.videoId = route.snapshot.queryParamMap.get('id') || '';
    console.log(this.videoId);
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next(true);
    this.ngUnsubscribe.complete();
  }
}
