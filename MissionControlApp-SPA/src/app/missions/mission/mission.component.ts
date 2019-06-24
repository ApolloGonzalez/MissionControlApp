import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-mission',
  templateUrl: './mission.component.html',
  styleUrls: ['./mission.component.css']
})
export class MissionComponent implements OnInit {

  missionId: any;

  constructor(private route: ActivatedRoute) {
    this.missionId = this.route.snapshot.paramMap.get( 'id' );
   }

  ngOnInit() {

  }

}
