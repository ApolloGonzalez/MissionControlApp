import {Routes} from '@angular/router';
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

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members', component: MemberListComponent,
                resolve: {users: MemberListResolver}},
            {
                path: 'missions',
                component: MissionListComponent,
                resolve: { missions: MissionListResolver }
            },
            {
                path: 'mission',
                component: MissionComponent,
                children: [
                    {
                        path: ':id',
                        component: MissionDetailComponent,
                        resolve: {mission: MissionDetailResolver}
                    },
                    {
                        path: ':id/assessment',
                        component: MissionAssessmentInsightsComponent
                    },
                    {
                        path: ':id/projectlifecycle',
                        component: MissionProjectLifecycleComponent
                    }
                ]
            },
            {path: 'members/:id', component: MemberDetailComponent,
                resolve: {user: MemberDetailResolver}},
            // {path: 'missions/:id', component: MissionComponent,
            //     resolve: {mission: MissionDetailResolver}},
            {path: 'member/edit', component: MemberEditComponent,
                resolve: {user: MemberEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages', component: MessagesComponent, resolve: {messages: MessagesResolver}},
            {path: 'lists', component: ListsComponent, resolve: {users: ListsResolver}},
            {path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin', 'Moderator']}},
        ]
    },
    {path: '**', redirectTo: '', pathMatch: 'full'},
];
