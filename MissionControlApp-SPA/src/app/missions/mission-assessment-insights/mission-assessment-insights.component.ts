import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Assessment } from 'src/app/_models/assessment';

@Component({
  selector: 'app-mission-assessment-insights',
  templateUrl: './mission-assessment-insights.component.html',
  styleUrls: ['./mission-assessment-insights.component.css']
})
export class MissionAssessmentInsightsComponent implements OnInit {
  missionAssessment: Assessment;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.missionAssessment = data['missionassessment'];
    });
  }

}
