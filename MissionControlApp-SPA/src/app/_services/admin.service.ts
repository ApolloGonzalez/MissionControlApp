import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '../../../node_modules/@angular/common/http';
import { User } from '../_models/user';
import { Mission } from '../_models/mission';
import { Assessment } from '../_models/assessment';

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
