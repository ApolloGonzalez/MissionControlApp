import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MissionService } from '../_services/mission.service';
import { Mission } from '../_models/mission';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MissionDetailResolver implements Resolve<Mission> {
    constructor(private authService: AuthService, private missionService: MissionService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Mission> {
        return this.missionService.getMission(this.authService.decodedToken.nameid, route.params['id'])
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/missions']);
                    return of(null);
                })
            );
        }
}
