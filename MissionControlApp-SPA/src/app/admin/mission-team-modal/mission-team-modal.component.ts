import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap';
import { Mission } from 'src/app/_models/mission';

@Component({
  selector: 'app-mission-team-modal',
  templateUrl: './mission-team-modal.component.html',
  styleUrls: ['./mission-team-modal.component.css']
})
export class MissionTeamModalComponent implements OnInit {
  @Output() updateSelectedTeamMembers = new EventEmitter();
  mission: Mission;
  missionTeamMembers: any[];

  constructor(public bsModalRef: BsModalRef) { }

  ngOnInit() {
  }

  updateTeamMembers() {
    this.updateSelectedTeamMembers.emit(this.missionTeamMembers);
    this.bsModalRef.hide();
  }

}
