import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule,
    ButtonsModule, TooltipModule, ModalModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { NgxGalleryModule } from 'ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
import { TimeAgoPipe } from 'time-ago-pipe';
import { OrderModule } from 'ngx-order-pipe';
import {NgxMaskModule, IConfig} from 'ngx-mask';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { PhotoEditorComponent } from './members/photo-editor/photo-editor.component';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { MemberMessagesComponent } from './members/member-messages/member-messages.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { PhotoManagementComponent } from './admin/photo-management/photo-management.component';
import { AdminService } from './_services/admin.service';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { MissionCreateComponent } from './missions/mission-create/mission-create.component';
import { MissionListResolver } from './_resolvers/mission-list.resolver';
import { MissionListComponent } from './missions/mission-list/mission-list.component';
import { MissionService } from './_services/mission.service';
import { MissionCardComponent } from './missions/mission-card/mission-card.component';
import { MissionDetailComponent } from './missions/mission-detail/mission-detail.component';
import { MissionDetailResolver } from './_resolvers/mission-detail.resolver';
import { MissionComponent } from './missions/mission/mission.component';
import { MissionNavComponent } from './missions/mission-nav/mission-nav.component';
import { MissionAssessmentInsightsComponent } from './missions/mission-assessment-insights/mission-assessment-insights.component';
import { MissionProjectLifecycleComponent } from './missions/mission-project-lifecycle/mission-project-lifecycle.component';
import { MissionCreateResolver } from './_resolvers/mission-create.resolver';
import { MissionTeamListComponent } from './missions/mission-team-list/mission-team-list.component';
import { MissionTeamListResolver } from './_resolvers/mission-team-list.resolver';
import { MissionTeamMemberCardComponent } from './missions/mission-team-member-card/mission-team-member-card.component';
import { MemberDashboardComponent } from './members/member-dashboard/member-dashboard.component';
import { MissionQueueComponent } from './admin/mission-queue/mission-queue.component';
import { AdminComponent } from './admin/admin/admin.component';
import { AdminNavComponent } from './admin/admin-nav/admin-nav.component';
import { UsersWithRolesResolver } from './_resolvers/users-with-roles.resolver';
import { MissionsQueueResolver } from './_resolvers/missions-queue.resolver';
import { MissionManagementComponent } from './admin/mission-management/mission-management.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';
import { MissionTeamManagementComponent } from './admin/mission-team-management/mission-team-management.component';
import { MissionAssessmentEditComponent } from './admin/mission-assessment-edit/mission-assessment-edit.component';
import { MissionCreateAssessmentComponent } from './admin/mission-create-assessment/mission-create-assessment.component';
import { MissionAssessmentResolver } from './_resolvers/mission-assessment.resolver';
import { MissionDashboardComponent } from './missions/mission-dashboard/mission-dashboard.component';
import { MissionManagementResolver } from './_resolvers/mission-management.resolver';
import { AcceleratorUseCaseModalComponent } from './missions/accelerator-use-case-modal/accelerator-use-case-modal.component';
import { TruncateTextPipe } from './_pipes/truncate-text.pipe';

export function tokenGetter() {
  return localStorage.getItem('token');
}

export let options: Partial<IConfig> | (() => Partial<IConfig>) = {};

export class CustomHammerConfig extends HammerGestureConfig  {
  overrides = {
      pinch: { enable: false },
      rotate: { enable: false }
  };
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MessagesComponent,
    MemberCardComponent,
    MemberDetailComponent,
    MemberEditComponent,
    MemberDashboardComponent,
    MissionComponent,
    MissionCreateComponent,
    MissionListComponent,
    MissionCardComponent,
    MissionDetailComponent,
    MissionNavComponent,
    MissionAssessmentInsightsComponent,
    MissionAssessmentEditComponent,
    MissionCreateAssessmentComponent,
    MissionProjectLifecycleComponent,
    MissionTeamListComponent,
    MissionTeamMemberCardComponent,
    MissionTeamManagementComponent,
    MissionQueueComponent,
    MissionManagementComponent,
    MissionDashboardComponent,
    PhotoEditorComponent,
    AcceleratorUseCaseModalComponent,
    TimeAgoPipe,
    MemberMessagesComponent,
    AdminPanelComponent,
    AdminComponent,
    AdminHomeComponent,
    AdminNavComponent,
    HasRoleDirective,
    UserManagementComponent,
    PhotoManagementComponent,
    RolesModalComponent,
    TruncateTextPipe
  ],
  imports: [
    BrowserModule,
    NgxMaskModule.forRoot(options),
    OrderModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    BsDatepickerModule.forRoot(),
    ButtonsModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    ModalModule.forRoot(),
    TooltipModule.forRoot(),
    NgxGalleryModule,
    FileUploadModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: ['localhost:5000'],
        blacklistedRoutes: ['localhost:5000/api/auth']
      }
    })
  ],
  providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard,
      UserService,
      MissionService,
      MemberDetailResolver,
      MemberListResolver,
      MemberEditResolver,
      PreventUnsavedChanges,
      ListsResolver,
      MessagesResolver,
      MissionListResolver,
      MissionDetailResolver,
      MissionCreateResolver,
      MissionTeamListResolver,
      MissionsQueueResolver,
      MissionAssessmentResolver,
      MissionManagementResolver,
      UsersWithRolesResolver,
      AdminService,
      { provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig }
    ],
  entryComponents: [
    RolesModalComponent,
    AcceleratorUseCaseModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
