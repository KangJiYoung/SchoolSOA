import { Component, OnInit, Input } from '@angular/core';
import { PostService } from '../../../services/post.service';
import { Post } from '../../../models/post.model';
import { Blog } from '../../../models/blog.model';
import {AlertController, NavParams} from 'ionic-angular';

@Component({
  selector: 'page-details',
  templateUrl: 'details.html',
  providers: [PostService]
})
export class DetailsPage implements OnInit {
  @Input() blog: Blog;

  posts: Post[] = [];

  constructor(
    private navParams: NavParams,
    private postService: PostService,
    private alertCtrl: AlertController) {
      this.blog = this.navParams.get('blog');
  }

  ngOnInit() {
    this.postService
      .getAllPosts(this.blog.id)
      .subscribe(posts => this.posts = posts);
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
                .addPost({ blogId: this.blog.id, content })
                .subscribe(post => this.posts.push(post));
            }
          }
        ]
      })
      .present();
  }
}
