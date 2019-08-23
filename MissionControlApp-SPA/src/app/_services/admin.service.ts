import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '../../../node_modules/@angular/common/http';
import { User } from '../_models/user';
import { Mission } from '../_models/mission';
import { Assessment } from '../_models/assessment';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMissionTeam(missionId: number) {
    return this.http.get(this.baseUrl + 'admin/missionTeam/' + missionId);
  }

  getMissionAssessment(missionAssessmentId: number) {
    return this.http.get(this.baseUrl + 'admin/missionassessment/' + missionAssessmentId);
  }

  getMissionEmployees() {
    return this.http.get(this.baseUrl + 'admin/missionemployees');
  }

  getUsersWithRoles() {
    return this.http.get(this.baseUrl + 'admin/userswithroles');
  }

  getMissionsInQueue(id: number, page?, itemsPerPage?, missionParams?): Observable<PaginatedResult<Mission[]>> {
    const paginatedResult: PaginatedResult<Mission[]> = new PaginatedResult<Mission[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    if (missionParams != null) {
      params = params.append('minAge', missionParams.minAge);
      params = params.append('maxAge', missionParams.maxAge);
      params = params.append('gender', missionParams.gender);
      params = params.append('orderBy', missionParams.orderBy);
    }

    return this.http.get<Mission[]>(this.baseUrl + 'admin/getmissionsinqueue', { observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }

  getMission(userId: number, id: number): Observable<Mission> {
    return this.http.get<Mission>(this.baseUrl + 'admin/mission/' + id);
  }

  updateUserRoles(user: User, roles: {}) {
    return this.http.post(this.baseUrl + 'admin/editRoles/' + user.userName, roles);
  }

  updateMissionTeamMembers(mission: Mission, missionTeamUserIds: {}) {
    return this.http.post(this.baseUrl + 'admin/editMissionTeamMembers/' + mission.missionId, missionTeamUserIds);
  }

  updateMissionAssessment(missionAssessment: Assessment) {
    return this.http.put(this.baseUrl + 'admin/editMissionAssessment', missionAssessment);
  }

  createMissionAssessment(missionAssessment: Assessment) {
    return this.http.post(this.baseUrl + 'admin/createmissionassessment', missionAssessment);
  }

  getPhotosForApproval() {
    return this.http.get(this.baseUrl + 'admin/photosForModeration');
  }

  approvePhoto(photoId) {
    return this.http.post(this.baseUrl + 'admin/approvePhoto/' + photoId, {});
  }

  rejectPhoto(photoId) {
    return this.http.post(this.baseUrl + 'admin/rejectPhoto/' + photoId, {});
  }
}
