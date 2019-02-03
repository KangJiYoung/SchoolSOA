import {Component, OnInit} from '@angular/core';

import { AuthenticationPage } from '../authentication/authentication';
import { BlogPage } from '../blog/blog';
import {AuthorizationService} from "../../services/authorization.service";

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage implements OnInit {

  tab1Root = BlogPage;
  tab2Root = AuthenticationPage;

  isAuth: boolean;

  constructor(private authorizationService: AuthorizationService) {
  }

  ngOnInit(): void {
    this.authorizationService
      .isAuthenticated
      .subscribe(isAuth => this.isAuth = isAuth);
  }
}
