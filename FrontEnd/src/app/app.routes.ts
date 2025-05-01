import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/auth/register/register.component';
import { HomeComponent } from './pages/home/home.component';
// import { authGuard, noAuthGuard } from './guards/auth.guard';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { ProfileComponent } from './pages/profile/profile.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { AllUserTableComponent } from './pages/all-user-table/all-user-table.component';
import { AdminComponent } from './pages/admin/admin.component';
import { ServererrorComponent } from './components/servererror/servererror.component';
import { MySubjectComponent } from './pages/my-subject/my-subject.component';
import { TranslocoComponent } from './pages/admin/transloco/transloco.component';
import { authGuard, noAuthGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: 'auth/register',
    component: RegisterComponent,
    canActivate: [noAuthGuard],
  },

  {
    path: '',
    component: HomeComponent,
    canActivate: [authGuard],
    canActivateChild: [authGuard],

    children: [
      {
        path: 'profile',
        component: ProfileComponent,
      },
      {
        path: 'admin',
        component:AdminComponent,
        children:[
          {
            path: 'transloco',
            component:TranslocoComponent
          }
        ]
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: 'admin',
        component: AdminComponent,
        children: [
          {
            path: 'allusers',
            component: AllUserTableComponent,
            children: [
              {
                path: 'admins',
                component: AllUserTableComponent,
              },
              {
                path: 'teachers',
                component: AllUserTableComponent,
              },
              {
                path: 'students',
                component: AllUserTableComponent,
              },
            ],
          },
        ],
      },
      {
        path: 'student',
        children: [
          {
            path: 'tests',
            loadChildren:()=>import('./pages/tests/tests.routing').then(x=>x.TESTS_ROUTES),
          },
          {
            path:"subjects",
            loadChildren:()=>import('./pages/subjects/subjects.routing').then(x=>x.SUBJECTS_ROUTES),
          },
        ],
      },
      {
        path:'teacher',
        children:[
          {
            path:'classes',
            loadChildren:()=>import("./pages/myclasses/myclass.routing").then(x=>x.MyClassRoute),
          },
          {
            path:'subjects',
            component:MySubjectComponent
          }
        ]
      }
    ],
  },
  {
    path:'server-error',
    component:ServererrorComponent
  },
  {
    path: '**',
    component: NotfoundComponent,
  },
];
