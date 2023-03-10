import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { TabsComponent } from './controls/tabs.component';
import { UserComponent } from './controls/user.component';
import { ConfirmationComponent } from './controls/confirmation.component';
import { HomeComponent } from './home/home.component';
import { TimeComponent } from './time/time.component';
import { ReportComponent } from './report/report.component';
import { AdminComponent } from './admin/admin.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TabsComponent,
    UserComponent,
    ConfirmationComponent,
    HomeComponent,
    TimeComponent,
    ReportComponent,
    AdminComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'time', component: TimeComponent },
      { path: 'report', component: ReportComponent },
      { path: 'admin', component: AdminComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
