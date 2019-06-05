import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Mission } from '../_models/mission';

@Injectable({
  providedIn: 'root'
})
export class MissionService {

  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMission(userId, id): Observable<Mission> {
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
}
