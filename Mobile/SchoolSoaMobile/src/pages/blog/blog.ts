import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../services/blog.service';
import { Blog } from '../../models/blog.model';
import { NavController } from 'ionic-angular';
import { DetailsPage } from './details/details';

@Component({
  selector: 'page-blog',
  templateUrl: 'blog.html',
  providers: [BlogService]
})
export class BlogPage implements OnInit {
  blogs: Blog[] = [];

  constructor(
    private blogService: BlogService,
    private navController: NavController) {
  }

  ngOnInit() {
    this.blogService
      .getAllBlogs(0, 3)
      .subscribe(blogs => this.blogs = blogs);
  }

  onBlogClick(blog: Blog) {
    this.navController.push(DetailsPage, { blog });
  }
}
