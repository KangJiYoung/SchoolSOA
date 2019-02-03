import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class BaseService {
    protected baseUrl: string;

    constructor(protected http: HttpClient) {
      this.baseUrl = 'http://localhost:5010/';
    }

    protected getToken(): string {
        return localStorage.getItem('token');
    }
}
