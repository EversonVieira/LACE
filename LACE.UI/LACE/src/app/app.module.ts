import { Injector, NgModule } from '@angular/core';
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
import { AppInjector } from 'src/shared/services/app-injector.service';
import { UserRegisterComponent } from './pages/users/user-register/user-register.component';
import { UserUpdateComponent } from './pages/users/user-update/user-update.component';
import { NgxMaskModule } from 'ngx-mask';
import { NgxUiLoaderHttpModule, NgxUiLoaderModule, NgxUiLoaderRouterModule } from 'ngx-ui-loader';
import { HttpConfiguration } from 'src/shared/models/HttpConfiguration';
import { Myhttpconfig } from './conf/myhttpconfig';
import { ToastrModule } from 'ngx-toastr';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HeaderLoginComponent } from './layout/components/header-login/header-login.component';
import { UploadComponent } from './pages/reports/upload/upload.component';
import { LoginComponent } from './pages/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent,
    ItemButtonComponent,
    HomeComponent,
    ReportsComponent,
    ConfigurationComponent,
    AboutComponent,
    UserDashboardComponent,
    UserRegisterComponent,
    UserUpdateComponent,
    HeaderLoginComponent,
    UploadComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserModule,
    NgxMaskModule.forRoot(),
    BrowserAnimationsModule,
    NgxUiLoaderModule,
    NgxUiLoaderRouterModule,
    HttpClientModule,
    NgxUiLoaderHttpModule,
    ReactiveFormsModule,
    CommonModule,
    ToastrModule.forRoot(), // ToastrModule added
    NgbModule
  ],
  providers: [
    { provide: HttpConfiguration, useClass: Myhttpconfig },
    { provide: AppInjector, useClass:AppInjector},

  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(injector: Injector) {
    AppInjector.setInjector(injector);
  }
}
