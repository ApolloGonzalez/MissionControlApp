import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Mission } from 'src/app/_models/mission';
import { UseCase } from 'src/app/_models/usecase';

@Component({
  selector: 'app-mission-management',
  templateUrl: './mission-management.component.html',
  styleUrls: ['./mission-management.component.css']
})
export class MissionManagementComponent implements OnInit {
  mission: Mission;
  useCases: UseCase[] = [];

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.mission = data['mission'];
    });

    this.mission.accelerators.forEach(accelerator => {
      const jsonDescription: any = JSON.parse(accelerator.description);
      const useCase = <UseCase>jsonDescription;
      this.mission.useCases = [];
      this.useCases.push(useCase);
    });
  }
}
