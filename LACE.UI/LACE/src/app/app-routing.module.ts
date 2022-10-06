import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { ConfigurationComponent } from './pages/configuration/configuration.component';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { UploadComponent } from './pages/reports/upload/upload.component';
import { UserDashboardComponent } from './pages/users/user-dashboard/user-dashboard.component';
import { UserRegisterComponent } from './pages/users/user-register/user-register.component';
import { UserUpdateComponent } from './pages/users/user-update/user-update.component';

const routes: Routes = [
  {path: "", redirectTo: "home", pathMatch: 'full' },
  {path: "''", redirectTo: "home" },
  {path: "home", component: HomeComponent,  pathMatch: 'full'},
  {path: "reports", component: ReportsComponent },
  {path: "reports/upload", component: UploadComponent },
  // {path: "dashboard/users", component: UserDashboardComponent },
  {path: "about", component: AboutComponent },
  {path: "login", component: LoginComponent},
  // {path: "configuration", component: ConfigurationComponent },
  {path: "user/register", component: UserRegisterComponent },
  {path: "user/update", component: UserUpdateComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
