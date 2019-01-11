import { Component } from '@angular/core';

import { AuthenticationPage } from '../authentication/authentication';
import { BlogPage } from '../blog/blog';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {

  tab1Root = BlogPage;
  tab2Root = AuthenticationPage;

  constructor() {

  }
}
