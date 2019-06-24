import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-mission-nav',
  templateUrl: './mission-nav.component.html',
  styleUrls: ['./mission-nav.component.css']
})
export class MissionNavComponent implements OnInit {

  @Input() missionId: any;

  constructor( ) {

  }

  ngOnInit() {
  }

}
