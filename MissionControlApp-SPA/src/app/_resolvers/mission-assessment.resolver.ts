import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MissionService } from '../_services/mission.service';
import { AuthService } from '../_services/auth.service';
import { Assessment } from '../_models/assessment';

@Injectable()
export class MissionAssessmentResolver implements Resolve<Assessment> {
    constructor(private authService: AuthService, private missionService: MissionService,
        private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Assessment> {
        return this.missionService
            .getMissionAssessment(this.authService.decodedToken.nameid, route.parent.params['id'])
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/missions']);
                    return of(null);
                })
            );
        }
}
