import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BaseService } from './base.service';
import { Observable } from 'rxjs/Observable';
import { Blog } from '../models/blog.model';
import { map } from 'rxjs/operators';

@Injectable()
export class BlogService extends BaseService {

    constructor(http: HttpClient) {
        super(http);
    }

    getAllBlogs(skip: number, take: number): Observable<Blog[]> {
        let params = new HttpParams();
        params = params.set('skip', skip.toString());
        params = params.set('take', take.toString());

        return this.http
            .get<Blog[]>('http://localhost:5000/' + 'blogs', {
                params
            })
            .pipe(map((response: any) => response.blogs));
    }
}