import {Component, OnInit} from '@angular/core';

import { AuthenticationPage } from '../authentication/authentication';
import { BlogPage } from '../blog/blog';
import {AuthorizationService} from "../../services/authorization.service";
import {ProfilePage} from "../profile/profile";

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage implements OnInit {

  blogPage = BlogPage;
  authPage = AuthenticationPage;
  profilePage = ProfilePage;

  isAuth: boolean;

  constructor(private authorizationService: AuthorizationService) {
  }

  ngOnInit(): void {
    this.authorizationService
      .isAuthenticated
      .subscribe(isAuth => this.isAuth = isAuth);
  }
}
