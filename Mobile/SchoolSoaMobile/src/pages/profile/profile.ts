import {Component, OnInit} from "@angular/core";
import {AuthorizationService} from "../../services/authorization.service";
import {User} from "../../models/user.model";
import {BadgeService} from "../../services/badge.service";
import {Badge} from "ionic-angular";

@Component({
  templateUrl: 'profile.html',
  providers: [BadgeService]
})
export class ProfilePage implements OnInit {
  user: User;
  badges: Badge[] = [];

  constructor(private authenticationService: AuthorizationService, private badgeService: BadgeService) { }

  ngOnInit() {
    this.authenticationService
      .currentUser
      .subscribe(user => this.user = user);
  }

  ionViewDidEnter() {
    this.badgeService
      .getMyBadges()
      .subscribe(badges => this.badges = badges);
  }

  changeProfile() {
    this.authenticationService
      .updateFullName({ fullname: this.user.fullname })
      .subscribe();
  }
}
