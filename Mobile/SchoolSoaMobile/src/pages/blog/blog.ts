import { Component, OnInit } from '@angular/core';
import { BlogService } from '../../services/blog.service';
import { Blog } from '../../models/blog.model';
import { NavController } from 'ionic-angular';
import { DetailsPage } from './details/details';
import {AuthorizationService} from "../../services/authorization.service";
import {BlogAddPage} from "./add/add";

@Component({
  selector: 'page-blog',
  templateUrl: 'blog.html',
  providers: [BlogService]
})
export class BlogPage implements OnInit {
  blogs: Blog[];
  isAuth: boolean;
  blogIndex: number;

  constructor(
    private blogService: BlogService,
    private navController: NavController,
    private authorizationService: AuthorizationService) {
  }

  ngOnInit() {
    this.authorizationService
      .isAuthenticated
      .subscribe(isAuth => this.isAuth = isAuth);
  }

  ionViewDidEnter(event) {
    this.blogs = [];
    this.blogIndex = 0;
    this.loadBlogs(event);
  }

  loadBlogs(event) {
    this.blogService
      .getAllBlogs(this.blogIndex * 20 + this.blogIndex++, 20)
      .subscribe(blogs => {
        this.blogs = this.blogs.concat(blogs);

        if (event) {
          if (blogs.length < 20) {
            event.enable(false);
          }
          event.complete();
        }
      });
  }

  onBlogClick(blog: Blog) {
    this.navController.push(DetailsPage, { blog });
  }

  addBlog() {
    this.navController.push(BlogAddPage);
  }
}
