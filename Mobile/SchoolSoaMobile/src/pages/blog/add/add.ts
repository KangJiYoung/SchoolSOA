import {Component} from "@angular/core";
import {InsertBlog} from "../../../models/blog.model";
import {BlogService} from "../../../services/blog.service";
import {AlertController, NavController} from "ionic-angular";

@Component({
  templateUrl: 'add.html',
  providers: [BlogService]
})
export class BlogAddPage {
  model: InsertBlog = new InsertBlog();

  constructor(
    private blogService: BlogService,
    private alertCtrl: AlertController,
    private navCtrl: NavController) { }

  addBlog() {
    this.blogService
      .insert(this.model)
      .subscribe(() => {
        this.alertCtrl
          .create({
            title: 'Blog created successfully',
            subTitle: 'Yay! Go ahead and see your blog post!',
            buttons: ['Ok']
          })
          .present()
          .then(() => this.navCtrl.pop());
      })
  }
}
