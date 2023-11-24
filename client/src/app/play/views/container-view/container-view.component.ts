import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AdminContainer } from 'src/app/_models/Adventure';
import { Container } from 'src/app/_models/AdventureSave';

@Component({
  selector: 'app-container-view',
  templateUrl: './container-view.component.html',
  styleUrls: ['./container-view.component.css'],
})
export class ContainerViewComponent implements OnInit {
  
  @Input() container: Container | undefined;
  @Output() backToLocationEvent = new EventEmitter();

  ngOnInit(): void {
   console.log('Container: ' + this.container);
  }

  backToLocation() {
    this.backToLocationEvent.emit();
  }
}
