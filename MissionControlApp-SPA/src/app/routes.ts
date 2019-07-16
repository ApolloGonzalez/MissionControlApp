import {Routes, ResolveEnd} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetailResolver } from './_resolvers/member-detail.resolver';
import { MemberListResolver } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { ListsResolver } from './_resolvers/lists.resolver';
import { MessagesResolver } from './_resolvers/messages.resolver';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { MissionListResolver } from './_resolvers/mission-list.resolver';
import { MissionListComponent } from './missions/mission-list/mission-list.component';
import { MissionDetailComponent } from './missions/mission-detail/mission-detail.component';
import { MissionDetailResolver } from './_resolvers/mission-detail.resolver';
import { MissionComponent } from './missions/mission/mission.component';
import { MissionAssessmentInsightsComponent } from './missions/mission-assessment-insights/mission-assessment-insights.component';
import { MissionProjectLifecycleComponent } from './missions/mission-project-lifecycle/mission-project-lifecycle.component';
import { MissionCreateComponent } from './missions/mission-create/mission-create.component';
import { MissionCreateResolver } from './_resolvers/mission-create.resolver';
import { MissionTeamListComponent } from './missions/mission-team-list/mission-team-list.component';
import { MissionTeamListResolver } from './_resolvers/mission-team-list.resolver';
import { MemberDashboardComponent } from './members/member-dashboard/member-dashboard.component';
import { AdminComponent } from './admin/admin/admin.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { MissionQueueComponent } from './admin/mission-queue/mission-queue.component';
import { UsersWithRolesResolver } from './_resolvers/users-with-roles.resolver';
import { MissionsQueueResolver } from './_resolvers/missions-queue.resolver';
import { MissionManagementComponent } from './admin/mission-management/mission-management.component';
import { AdminHomeComponent } from './admin/admin-home/admin-home.component';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent,
                resolve: {users: MemberListResolver}
            },
            {path: 'memberdashboard', component: MemberDashboardComponent/* ,
                resolve: {users: MemberListResolver} */
            },
            {
                path: 'missions/create',
                component: MissionCreateComponent,
                resolve: { missioncreateformlists: MissionCreateResolver }
            },
            {
                path: 'missions',
                component: MissionListComponent,
                resolve: { missions: MissionListResolver }
            },
            {
                path: 'mission/:id',
                component: MissionComponent,
                children: [
                    {
                        path: '',
                        component: MissionDetailComponent,
                        resolve: {mission: MissionDetailResolver}
                    },
                    {
                        path: 'assessment',
                        component: MissionAssessmentInsightsComponent
                    },
                    {
                        path: 'missionlifecycle',
                        component: MissionProjectLifecycleComponent
                    },
                    {
                        path: 'team',
                        component: MissionTeamListComponent,
                        resolve: {missionteammembers: MissionTeamListResolver}
                    }
                ]
            },
            {path: 'members/:id', component: MemberDetailComponent,
                resolve: {user: MemberDetailResolver}},
            {path: 'member/edit', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
            {path: 'lists', component: ListsComponent, resolve: {users: ListsResolver}},
            {
                path: 'admin',
                /* component: AdminPanelComponent, */
                component: AdminComponent,
                /* resolve: { missions: MissionListResolver }, */
                data:
                {
                    roles: ['Admin', 'Moderator', 'MissionAdmin', 'UserManager']
                },
                children: [
                    {
                        path: '',
                        component: AdminHomeComponent,
                        data:
                        {
                            roles: ['Admin', 'UserManager', 'MissionAdmin']
                        }
                    },
                    {
                        path: 'usermanagement',
                        component: UserManagementComponent,
                        resolve: { users: UsersWithRolesResolver },
                        data:
                        {
                            roles: ['Admin', 'UserManager']
                        }
                    },
                    {
                        path: 'missionqueue',
                        component: MissionQueueComponent,
                        resolve: { missions: MissionsQueueResolver },
                        data:
                        {
                            roles: ['Admin', 'MissionAdmin']
                        }
                    },
                    {
                        path: 'missionmanagement/:id',
                        component: MissionManagementComponent,
                        resolve: { mission: MissionDetailResolver },
                        data:
                        {
                            roles: ['Admin', 'MissionAdmin']
                        }
                    }
                ]
            }
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
