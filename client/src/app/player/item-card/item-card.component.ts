import { Component, EventEmitter, Input, Output } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Item } from 'src/app/_models/item';
import { PlayerAttackComponent } from 'src/app/play/modals/player-attack/player-attack.component';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css', '../../app.component.css'],
})
export class ItemCardComponent {
  @Input() item: Item | undefined;
  @Output() selectRequestEvent = new EventEmitter();

  constructor(
    public bsModalRef: BsModalRef,
    private bsModalService: BsModalService
  ) {}

  attack() {
    this.bsModalRef = this.bsModalService.show(PlayerAttackComponent);
    this.bsModalRef.content.setItem(this.item);
    this.bsModalRef.onHidden?.subscribe({});
  }

  selectRequest() {
    this.selectRequestEvent.emit();
  }
}
