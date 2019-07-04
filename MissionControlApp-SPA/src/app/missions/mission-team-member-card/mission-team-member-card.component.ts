import { Component, OnInit, Input } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { UserService } from 'src/app/_services/user.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { MissionTeamMember } from 'src/app/_models/missionteammember';

@Component({
  selector: 'app-mission-team-member-card',
  templateUrl: './mission-team-member-card.component.html',
  styleUrls: ['./mission-team-member-card.component.css']
})
export class MissionTeamMemberCardComponent implements OnInit {
  @Input() teamMember: MissionTeamMember;

  constructor(private authService: AuthService,
      private userService: UserService,
      private alertify: AlertifyService) { }

  ngOnInit() {
  }

}
