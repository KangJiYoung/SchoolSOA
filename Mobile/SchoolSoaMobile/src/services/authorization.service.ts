import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BaseService} from './base.service';
import {Observable} from 'rxjs/Observable';
import {RegisterModel} from "../models/register.model";
import {BehaviorSubject, ReplaySubject} from "rxjs";
import {User} from "../models/user.model";
import {distinctUntilChanged, map} from "rxjs/operators";
import {LoginModel} from "../models/login.model";

@Injectable()
export class AuthorizationService extends BaseService {
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());

  private isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor(http: HttpClient) {
    super(http);
  }

  initialize() {
    if (localStorage.getItem('token')) {
      this.http
        .get<User>(this.baseUrl + 'me')
        .subscribe(user => this.setAuth(user),
          error => this.removeAuth());
    }
  }

  register(model: RegisterModel): Observable<User> {
    return this.http
      .post<User>(this.baseUrl + 'register', model)
      .pipe(map(user => {
        this.setAuth(user);

        return user;
      }));
  }

  login(model: LoginModel): Observable<User> {
    return this.http
      .post<User>(this.baseUrl + 'login', model)
      .pipe(map(user => {
        this.setAuth(user);

        return user;
      }));
  }

  setAuth(user: User) {
    localStorage.setItem('token', user.token);

    this.currentUserSubject.next(user);
    this.isAuthenticatedSubject.next(true);
  }

  removeAuth() {
    localStorage.removeItem('token');

    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);
  }
}
