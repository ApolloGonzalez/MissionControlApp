import { Component, OnInit, Input } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-mission-create-assessment',
  templateUrl: './mission-create-assessment.component.html',
  styleUrls: ['./mission-create-assessment.component.css']
})
export class MissionCreateAssessmentComponent implements OnInit {
  @Input() missionId: number;
  missionAssessmentForm: FormGroup;

  constructor(private authService: AuthService, private adminService: AdminService, private router: Router,
    private alertify: AlertifyService, private route: ActivatedRoute, private fb: FormBuilder) { }

  ngOnInit() {
    this.createMissionAssessmentForm();
  }

  createMissionAssessmentForm() {

    this.missionAssessmentForm = this.fb.group({
      dataLocation: [''],
      dataType: [''],
      dataDomainExpert: [''],
      dataQuality: [''],
      challengeSolution: [''],
      challengeType: [''],
      infrastructureRequirement: [''],
      accuracyRequirement: [''],
      missionId: [''],
      userId: ['']
    });
  }

}
