import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Mission } from '../_models/mission';
import { AuthService } from '../_services/auth.service';
import { AdminService } from '../_services/admin.service';

@Injectable()
export class MissionManagementResolver implements Resolve<Mission> {
    constructor(private authService: AuthService, private adminService: AdminService,
        private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Mission> {
        return this.adminService.getMission(this.authService.decodedToken.nameid, route.params['id'])
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['admin/missionqueue']);
                    return of(null);
                })
            );
        }
}
