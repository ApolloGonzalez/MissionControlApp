import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Mission } from 'src/app/_models/mission';
import { BsModalService, BsModalRef } from '../../../../node_modules/ngx-bootstrap';
import { AdminService } from 'src/app/_services/admin.service';
import { MissionTeamModalComponent } from '../mission-team-modal/mission-team-modal.component';
import { MissionTeamMember } from 'src/app/_models/missionteammember';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-mission-management',
  templateUrl: './mission-management.component.html',
  styleUrls: ['./mission-management.component.css']
})
export class MissionManagementComponent implements OnInit {
  bsModalRef: BsModalRef;
  mission: Mission;
  // missionTeamMembers: MissionTeamMember[];
  // users: User[];

  constructor(private adminService: AdminService, private alertify: AlertifyService,
    private route: ActivatedRoute, private modalService: BsModalService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.mission = data['mission'];
    });
  }

  editMissionTeamModal(mission: Mission) {
    const initialState = {
      /* user,
      roles: this.getRolesArray(user) */
      mission,
      // missionTeamMembers: this.getMissionTeamMembers(mission.missionId)
      missionTeamMembers: this.getMissionTeamMembers(mission)
    };

    this.bsModalRef = this.modalService.show(MissionTeamModalComponent, { initialState });

    /*  this.bsModalRef.content.updateSelectedTeamMembers.subscribe((values) => {
       const teamMembersToUpdate = {
         teamMembers: [...values.filter(el => el.checked === true).map(el => el.name)]
       };
        if (teamMembersToUpdate) {
         this.adminService.updateUserRoles(mission, teamMembersToUpdate).subscribe(() => {
           mission.missionTeam = [...teamMembersToUpdate.roleNames];
         }, error => {
           console.log(error);
         });
       } 
     }); */
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

    /*     const teamMembers = [];

        this.adminService.getMissionTeam(missionId).subscribe((missionTeamMembers: MissionTeamMember[]) => {
          const missionTeam = missionTeamMembers;
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
        }, error => {
          console.log(error);
        }); */

    return teamMembers;
  }

  private getRolesArray(user) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Member', value: 'Member' },
      { name: 'VIP', value: 'VIP' },
      { name: 'MissionAdmin', value: 'MissionAdmin' },
      { name: 'UserManager', value: 'UserManager' }
    ];

    for (let i = 0; i < availableRoles.length; i++) {
      let isMatch = false;
      for (let j = 0; j < userRoles.length; j++) {
        if (availableRoles[i].name === userRoles[j]) {
          isMatch = true;
          availableRoles[i].checked = true;
          roles.push(availableRoles[i]);
          break;
        }
      }
      if (!isMatch) {
        availableRoles[i].checked = false;
        roles.push(availableRoles[i]);
      }
    }
    return roles;
  }
}
