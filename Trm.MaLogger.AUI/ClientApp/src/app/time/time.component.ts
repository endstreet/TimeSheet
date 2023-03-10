import { Component } from '@angular/core';

@Component({
  selector: 'app-time-component',
  templateUrl: './time.component.html'
})
export class TimeComponent {
  public currentCount = 0;

  public incrementCounter() {
    this.currentCount++;
  }
}
