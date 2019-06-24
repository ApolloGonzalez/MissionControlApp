import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { Pagination, PaginatedResult } from 'src/app/_models/pagination';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { MissionService } from 'src/app/_services/mission.service';
import { Mission } from 'src/app/_models/mission';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-mission-list',
  templateUrl: './mission-list.component.html',
  styleUrls: ['./mission-list.component.css']
})
export class MissionListComponent implements OnInit {

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
