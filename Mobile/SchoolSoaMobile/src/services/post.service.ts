import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs/Observable";
import { map } from "rxjs/operators";
import { Post } from "../models/post.model";

@Injectable()
export class PostService extends BaseService {

    constructor(http: HttpClient) {
        super(http);
    }

    getAllPosts(blogId: string): Observable<Post[]> {
        let params = new HttpParams();
        params = params.set('blogId', blogId);

        return this.http
            .get<Post[]>('http://localhost:5000/' + 'blogPosts', {
                params
            })
            .pipe(map((response: any) => response.posts));
    }
}