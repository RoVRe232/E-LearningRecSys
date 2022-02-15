import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-result-card',
  templateUrl: './result-card.component.html',
  styleUrls: ['./result-card.component.scss'],
})
export class ResultCardComponent {
  @Input() title = 'Video Title';
  @Input() description = 'Result description';
  @Input() author = 'Author';
  @Input() thumbnailUrl =
    'https://user-images.githubusercontent.com/101482/29592647-40da86ca-875a-11e7-8bc3-941700b0a323.png';
  @Input() duration = 0;
}
