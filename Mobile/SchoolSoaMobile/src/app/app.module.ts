import {NgModule, ErrorHandler} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {IonicApp, IonicModule, IonicErrorHandler} from 'ionic-angular';
import {MyApp} from './app.component';

import {AuthenticationPage} from '../pages/authentication/authentication';
import {BlogPage} from '../pages/blog/blog';
import {TabsPage} from '../pages/tabs/tabs';
import {DetailsPage} from '../pages/blog/details/details';

import {StatusBar} from '@ionic-native/status-bar';
import {SplashScreen} from '@ionic-native/splash-screen';

import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {TokenInterceptor} from "../interceptors/authorization.interceptor";
import {AuthorizationService} from "../services/authorization.service";
import {ProfilePage} from "../pages/profile/profile";
import {BlogAddPage} from "../pages/blog/add/add";

@NgModule({
  declarations: [
    MyApp,
    AuthenticationPage,
    BlogPage,
    TabsPage,
    DetailsPage,
    ProfilePage,
    BlogAddPage
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    AuthenticationPage,
    BlogPage,
    TabsPage,
    DetailsPage,
    ProfilePage,
    BlogAddPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    {provide: ErrorHandler, useClass: IonicErrorHandler},
    {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true},
    AuthorizationService
  ]
})
export class AppModule {
}
