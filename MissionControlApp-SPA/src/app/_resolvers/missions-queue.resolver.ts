import {Injectable} from '@angular/core';
import {Mission} from '../_models/mission';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { MissionService } from '../_services/mission.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';

@Injectable()
export class MissionsQueueResolver implements Resolve<Mission[]> {
    pageNumber = 1;
    pageSize = 10;

    constructor(private authService: AuthService, private missionService: MissionService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Mission[]> {
        return this.missionService.getMissionsInQueue(this.authService.decodedToken.nameid,
                this.pageNumber,
                this.pageSize)
            .pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/admin']);
                return of(null);
            })
        );
    }
}
