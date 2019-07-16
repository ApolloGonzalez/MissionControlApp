import { Component, OnInit } from '@angular/core';
import { MissionService } from 'src/app/_services/mission.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Mission } from 'src/app/_models/mission';

@Component({
  selector: 'app-mission-management',
  templateUrl: './mission-management.component.html',
  styleUrls: ['./mission-management.component.css']
})
export class MissionManagementComponent implements OnInit {

  mission: Mission;

  constructor(private missionService: MissionService, private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.mission = data['mission'];
    });
  }

}
