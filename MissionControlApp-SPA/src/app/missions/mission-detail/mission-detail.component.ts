import { Component, OnInit } from '@angular/core';
import { MissionService } from 'src/app/_services/mission.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Mission } from 'src/app/_models/mission';
import { UseCase } from 'src/app/_models/usecase';
import { FormArray } from '@angular/forms';

@Component({
  selector: 'app-mission-detail',
  templateUrl: './mission-detail.component.html',
  styleUrls: ['./mission-detail.component.css']
})
export class MissionDetailComponent implements OnInit {

  mission: Mission;
  useCases: UseCase[] = [];

  constructor(private missionService: MissionService, private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.mission = data['mission'];

      this.mission.accelerators.forEach(accelerator => {
        const jsonDescription: any = JSON.parse(accelerator.description);
        const useCase = <UseCase>jsonDescription;
        this.mission.useCases = [];
        this.useCases.push(useCase);
      });
    });
  }

}
