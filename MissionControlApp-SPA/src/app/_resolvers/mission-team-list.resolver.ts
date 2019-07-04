import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MissionService } from '../_services/mission.service';
import { AuthService } from '../_services/auth.service';
import { MissionTeamMember } from '../_models/missionteammember';

@Injectable()
export class MissionTeamListResolver implements Resolve<MissionTeamMember[]> {

    constructor(private missionService: MissionService, private router: Router,
        private authService: AuthService, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<MissionTeamMember[]> {
        return this.missionService
            .getMissionTeam(this.authService.decodedToken.nameid, route.parent.params['id'])
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/missions']);
                    return of(null);
            })
        );
    }
}
