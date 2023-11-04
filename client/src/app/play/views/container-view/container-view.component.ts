import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AdminContainer } from 'src/app/_models/Adventure';

@Component({
  selector: 'app-container-view',
  templateUrl: './container-view.component.html',
  styleUrls: ['./container-view.component.css'],
})
export class ContainerViewComponent {
  @Input() container: AdminContainer | undefined;
  @Output() backToLocationEvent = new EventEmitter();

  backToLocation() {
    this.backToLocationEvent.emit();
  }
}
