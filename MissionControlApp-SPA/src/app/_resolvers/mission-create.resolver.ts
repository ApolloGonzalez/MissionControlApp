import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { MissionService } from '../_services/mission.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { MissionCreateFormLists } from '../_models/missioncreateformlists';

@Injectable()

export class MissionCreateResolver implements Resolve<MissionCreateFormLists> {

    constructor(private authService: AuthService, private missionService: MissionService, private router: Router,
        private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<MissionCreateFormLists> {
        return this.missionService.getMissionCreateFormLists(this.authService.decodedToken.nameid)
            .pipe(
            catchError(error => {
                this.alertify.error('Problem retrieving data');
                this.router.navigate(['/missions']);
                return of(null);
            })
        );
    }
}
