import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-item-card',
  templateUrl: './item-card.component.html',
  styleUrls: ['./item-card.component.css','../../app.component.css'],
})
export class ItemCardComponent {
  @Input() ItemName = '';
  @Input() ArmorValue = 0;
  @Input() AttackValue = 0;
  @Input() PhotoUrl = '';

}
