import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LayoutComponent } from './layout/layout/layout.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ItemButtonComponent } from './layout/components/item-button/item-button.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { ReportsComponent } from './pages/reports/reports.component';
import { ConfigurationComponent } from './pages/configuration/configuration.component';
import { AboutComponent } from './pages/about/about.component';
import { UserDashboardComponent } from './pages/users/user-dashboard/user-dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    ItemButtonComponent,
    HomeComponent,
    ReportsComponent,
    ConfigurationComponent,
    AboutComponent,
    UserDashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    CommonModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
