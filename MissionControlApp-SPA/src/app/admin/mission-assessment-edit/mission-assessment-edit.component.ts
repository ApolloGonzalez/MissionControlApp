import { Component, OnInit, Input, ViewChild, HostListener } from '@angular/core';
import { Assessment } from 'src/app/_models/assessment';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-mission-assessment-edit',
  templateUrl: './mission-assessment-edit.component.html',
  styleUrls: ['./mission-assessment-edit.component.css']
})
export class MissionAssessmentEditComponent implements OnInit {
  @Input() missionAssessment: Assessment;
  @ViewChild('editMissionAssessmentForm') editMissionAssessmentForm: NgForm;
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editMissionAssessmentForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(private authService: AuthService,
    private adminService: AdminService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  updateMissionAssessment() {
    this.adminService
        .updateMissionAssessment(this.missionAssessment)
        .subscribe(next => {
      this.alertify.success('Mission Assessment updated successfully');
      this.editMissionAssessmentForm.reset(this.missionAssessment);
    }, error => {
      this.alertify.error(error);
    });
  }

}
