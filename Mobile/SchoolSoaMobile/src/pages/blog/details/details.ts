import { Component, OnInit, Input } from '@angular/core';
import { PostService } from '../../../services/post.service';
import { Post } from '../../../models/post.model';
import { Blog } from '../../../models/blog.model';
import { NavParams } from 'ionic-angular';

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
    private postService: PostService) {
      this.blog = this.navParams.get('blog');
  }

  ngOnInit() {
    this.postService
      .getAllPosts(this.blog.id)
      .subscribe(posts => this.posts = posts);
  }
}
