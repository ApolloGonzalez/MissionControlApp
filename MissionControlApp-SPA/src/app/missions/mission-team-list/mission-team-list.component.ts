import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { MissionService } from 'src/app/_services/mission.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';
import { MissionTeamMember } from 'src/app/_models/missionteammember';

@Component({
  selector: 'app-mission-team-list',
  templateUrl: './mission-team-list.component.html',
  styleUrls: ['./mission-team-list.component.css']
})
export class MissionTeamListComponent implements OnInit {
  teamMembers: MissionTeamMember[];

  constructor(private missionService: MissionService, private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.teamMembers = data['missionteammembers'];
    });
    }
}
