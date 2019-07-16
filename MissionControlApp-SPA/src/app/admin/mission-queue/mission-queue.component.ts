import { Component, OnInit } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { Mission } from 'src/app/_models/mission';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_services/auth.service';
import { MissionService } from 'src/app/_services/mission.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-mission-queue',
  templateUrl: './mission-queue.component.html',
  styleUrls: ['./mission-queue.component.css']
})
export class MissionQueueComponent implements OnInit {

  missions: Mission[];
  user: User = JSON.parse(localStorage.getItem('user'));
  missionParams: any = {};
  pagination: Pagination;

  constructor(private authService: AuthService, private missionService: MissionService,
    private alertify: AlertifyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.missions = data['missions'].result;
      this.pagination = data['missions'].pagination;
    });

    this.missionParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.missionParams.minAge = 18;
    this.missionParams.maxAge = 99;
    this.missionParams.orderBy = 'lastActive';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMissions();
  }

  resetFilters() {
    this.missionParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.missionParams.minAge = 18;
    this.missionParams.maxAge = 99;
    this.loadMissions();
  }

  loadMissions() {
     this.missionService.getMissions(this.authService.decodedToken.nameid,
        this.pagination.currentPage,
        this.pagination.itemsPerPage,
        this.missionParams)
      .subscribe((res: PaginatedResult<Mission[]>) => {
        this.missions = res.result;
        this.pagination = res.pagination;
    }, error => {
      this.alertify.error(error);
    });
  }
}
