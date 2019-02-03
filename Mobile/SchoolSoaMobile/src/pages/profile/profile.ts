import {Component, OnInit} from "@angular/core";
import {AuthorizationService} from "../../services/authorization.service";
import {User} from "../../models/user.model";

@Component({
  templateUrl: 'profile.html'
})
export class ProfilePage implements OnInit {
  user: User;

  constructor(private authenticationService: AuthorizationService) { }

  ngOnInit() {
    this.authenticationService
      .currentUser
      .subscribe(user => this.user = user);
  }

  changeProfile() {
    this.authenticationService
      .updateFullName({ fullname: this.user.fullname })
      .subscribe();
  }
}
