import { Component, OnInit, Input } from '@angular/core';
import { Mission } from 'src/app/_models/mission';
import { AdminService } from 'src/app/_services/admin.service';
import { MissionTeamMember } from 'src/app/_models/missionteammember';

@Component({
  selector: 'app-mission-team-management',
  templateUrl: './mission-team-management.component.html',
  styleUrls: ['./mission-team-management.component.css']
})
export class MissionTeamManagementComponent implements OnInit {
  @Input() mission: Mission;
  missionTeamMembers: MissionTeamMember[];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.missionTeamMembers = this.getMissionTeamMembers(this.mission);
  }

  private getMissionTeamMembers(mission: Mission) {
    const teamMembers = [];
    const missionTeam = mission.missionTeam;

    this.adminService.getMissionEmployees().subscribe((users: any[]) => {
      const availableMissionEmployees = users;

      for (let i = 0; i < availableMissionEmployees.length; i++) {
        let isMatch = false;
        for (let j = 0; j < missionTeam.length; j++) {
          if (availableMissionEmployees[i].id === missionTeam[j].userId) {
            isMatch = true;
            availableMissionEmployees[i].checked = true;
            teamMembers.push(availableMissionEmployees[i]);
            break;
          }
        }
        if (!isMatch) {
          availableMissionEmployees[i].checked = false;
          teamMembers.push(availableMissionEmployees[i]);
        }
      }
    }, error => {
      console.log(error);
    });
    return teamMembers;
  }
}
