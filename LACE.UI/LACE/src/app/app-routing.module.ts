import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { ConfigurationComponent } from './pages/configuration/configuration.component';
import { HomeComponent } from './pages/home/home.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { UserDashboardComponent } from './pages/users/user-dashboard/user-dashboard.component';

const routes: Routes = [
  {path: "", redirectTo: "home", pathMatch: 'full' },
  {path: "''", redirectTo: "home" },
  {path: "home", component: HomeComponent,  pathMatch: 'full'},
  {path: "reports", component: ReportsComponent },
  {path: "dashboard/users", component: UserDashboardComponent },
  {path: "about", component: AboutComponent },
  {path: "configuration", component: ConfigurationComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
