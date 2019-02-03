import {Component} from '@angular/core';
import {AuthorizationService} from "../../services/authorization.service";
import {RegisterModel} from "../../models/register.model";
import {AlertController, NavController} from "ionic-angular";
import {LoginModel} from "../../models/login.model";

@Component({
  selector: 'page-authentication',
  templateUrl: 'authentication.html'
})
export class AuthenticationPage {
  loginModel = new LoginModel();
  loginErrors: string;

  registerModel = new RegisterModel();
  registerErrors: string;

  isLogin: boolean = true;

  constructor(private authorizationService: AuthorizationService, private alertCtrl: AlertController,
              private navCtrl: NavController) {
  }

  register() {
    this.authorizationService
      .register(this.registerModel)
      .subscribe(response => {
        this.alertCtrl
          .create({
            title: 'Registration successfully',
            subTitle: 'Yay! Go ahead and create your first blog post!',
            buttons: ['Ok']
          })
          .present();
      }, response => this.registerErrors = response.error.errors);
  }

  login() {
    this.authorizationService
      .login(this.loginModel)
      .subscribe(response => {
        this.alertCtrl
          .create({
            title: 'Login successfully',
            subTitle: 'Yay! Welcome back!',
            buttons: ['Ok']
          })
          .present();
      },
        response => this.loginErrors = response.error.errors);
  }
}
