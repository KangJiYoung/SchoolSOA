import {Component, OnInit, Input} from '@angular/core';
import {PostService} from '../../../services/post.service';
import {Post} from '../../../models/post.model';
import {Blog} from '../../../models/blog.model';
import {AlertController, NavParams} from 'ionic-angular';
import {AuthorizationService} from "../../../services/authorization.service";

@Component({
  selector: 'page-details',
  templateUrl: 'details.html',
  providers: [PostService]
})
export class DetailsPage implements OnInit {
  @Input() blog: Blog;

  posts: Post[] = [];
  isAuth: boolean;

  constructor(
    private navParams: NavParams,
    private postService: PostService,
    private alertCtrl: AlertController,
    private authorizationService: AuthorizationService) {
    this.blog = this.navParams.get('blog');
  }

  ngOnInit() {
    this.postService
      .getAllPosts(this.blog.id)
      .subscribe(posts => this.posts = posts);
    this.authorizationService
      .isAuthenticated
      .subscribe(isAuth => this.isAuth = isAuth);
  }

  addComment() {
    this.alertCtrl
      .create({
        title: 'Add Comment',
        inputs: [
          {
            type: 'text',
            name: 'content',
            placeholder: 'Comment'
          }
        ],
        buttons: [
          {
            text: 'Cancel',
            cssClass: 'secondary',
          },
          {
            text: 'Add',
            handler: event => {
              const content = event.content;
              if (!content) {
                alert('A comment cannot be empty!');
                return;
              }

              this.postService
                .addPost({blogId: this.blog.id, content})
                .subscribe(post => this.posts.push(post));
            }
          }
        ]
      })
      .present();
  }
}
