import {Injectable} from "@angular/core";
import {BaseService} from "./base.service";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs/Observable";
import {InsertPost, Post} from "../models/post.model";
import {Badge} from "ionic-angular";
import {map} from "rxjs/operators";

@Injectable()
export class BadgeService extends BaseService {

  constructor(http: HttpClient) {
    super(http);
  }

  getMyBadges(): Observable<Badge[]> {
    return this.http
      .get<Badge[]>(this.baseUrl + 'me/badges')
      .pipe(map((response: any) => response.badges));
  }
}
