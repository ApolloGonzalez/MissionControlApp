import { Component, OnInit, Input } from '@angular/core';
import { Mission } from 'src/app/_models/mission';

@Component({
  selector: 'app-mission-card',
  templateUrl: './mission-card.component.html',
  styleUrls: ['./mission-card.component.css']
})
export class MissionCardComponent implements OnInit {
  @Input() mission: Mission;
  constructor() { }

  ngOnInit() {
  }

}
