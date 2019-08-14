import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { Assessment } from 'src/app/_models/assessment';

@Component({
  selector: 'app-mission-create-assessment',
  templateUrl: './mission-create-assessment.component.html',
  styleUrls: ['./mission-create-assessment.component.css']
})
export class MissionCreateAssessmentComponent implements OnInit {
  @Input() missionId: number;
  @Output() cancelCreateMissionAssessment = new EventEmitter();
  missionAssessmentForm: FormGroup;
  missionAssessment: Assessment;

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
      missionId: this.missionId,
      userId: this.authService.decodedToken.nameid
    });
  }

  createMissionAssessment() {
    // if (this.missionAssessmentForm.valid) {
      this.missionAssessment = Object.assign({}, this.missionAssessmentForm.value);

      /* this.adminService
        .createMissionAssessment(this.missionAssessment).subscribe(() => {
          this.alertify.success('Mission Assessment Created Successfully');
          this.router.navigate(['/missionmanagement/' + this.missionAssessment.id]);
        }, error => {
          this.alertify.error(error);
        }); */
  //  }
      this.router.navigate(['admin/missionmanagement/' + this.missionAssessment.missionId]);
      console.log(this.missionAssessment);
  }

  cancel() {
    this.cancelCreateMissionAssessment.emit(false);
  }

}
