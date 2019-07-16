import {Injectable} from '@angular/core';
import {Resolve, Router, ActivatedRouteSnapshot} from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';
import { AdminService } from '../_services/admin.service';

@Injectable()
export class UsersWithRolesResolver implements Resolve<User[]> {

    constructor(private adminService: AdminService, private router: Router,
        private authService: AuthService, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<User[]> {
        return this.adminService
            .getUsersWithRoles()
            .pipe(
                catchError(error => {
                    this.alertify.error('Problem retrieving data');
                    this.router.navigate(['/admin']);
                    return of(null);
            })
        );
    }
}
