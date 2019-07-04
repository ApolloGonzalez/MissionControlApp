import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Mission } from '../_models/mission';
import { Platform } from '../_models/platform';
import { MissionCreateFormLists } from '../_models/missioncreateformlists';
import { MissionTeamMember } from '../_models/missionteammember';

@Injectable({
  providedIn: 'root'
})
export class MissionService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMissionTeam(userId: number, id: number): Observable<MissionTeamMember> {
    return this.http
      .get<MissionTeamMember>(this.baseUrl + 'users/' + userId + '/missions/' + id + '/missionteam');
  }

  getMission(userId: number, id: number): Observable<Mission> {
    return this.http.get<Mission>(this.baseUrl + 'users/' + userId + '/missions/' + id);
  }

  getMissions(id: number, page?, itemsPerPage?, missionParams?): Observable<PaginatedResult<Mission[]>> {
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

    return this.http.get<Mission[]>(this.baseUrl + 'users/' + id + '/missions', { observe: 'response', params})
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

  getMissionCreateFormLists(userId: number): Observable<MissionCreateFormLists> {
    return this.http
      .get<MissionCreateFormLists>(this.baseUrl + 'users/' + userId + '/missions/missioncreateformlists');
  }

  getAcceleratorsByIndustryAndBusinessId(userId: number, businessFunctionId: number, industryId: number) {
    return this.http
      .get
        (this.baseUrl + 'users/' + userId + '/missions/businessfunction/' + businessFunctionId + '/industry/' + industryId);
  }

  getPlatforms(userId: number): Observable<Platform> {
    return this.http
      .get<Platform>(this.baseUrl + 'users/' + userId + '/missions/platforms');
  }

  createMission(userId: number, mission: Mission) {
    return this.http.post(this.baseUrl + 'users/' + userId + '/missions/createmission', mission);
  }
}
