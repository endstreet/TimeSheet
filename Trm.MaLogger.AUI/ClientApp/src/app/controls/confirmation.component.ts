import { Component, Input } from '@angular/core';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'confirmation',
  template: `<div class="modal" tabindex = "-1" style = "display:block;" role = "dialog" >
  <div class="modal-dialog modal-sm" >
    <div class="modal-content" >
      <div class="modal-header modal-sm" style = "background-color: dodgerblue" >
        <h5 class="modal-title" >confirmation.title</h5>
          < /div>
          < div class="modal-body" >
            <div>
            <p>confirmation.message</p>
              </div>-
              < br />
              <div class="modal-footer" >
                <button type="button" class="btn btn-secondary btn-secondary-sm" (click)="onConfirm(false)" > Cancel < /button>
                <button class="btn btn-primary btn-secondary-sm active" (click)="onConfirm(true)" > Ok < /button>
         < /div>
      < /div>
    < /div>
  < /div>
< /div>`
})

export class ConfirmationComponent {
  @Input() title: string | undefined;
  @Input() message: string | undefined;
  @Output() ok = new EventEmitter<boolean>();

  onConfirm(value: boolean ) {
    this.ok.emit(value);
  }
}
