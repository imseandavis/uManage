import { Injectable } from 'angular2/core';
import { Http, Response, Headers } from 'angular2/http';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';
import { CONFIGURATION } from './../app.constants';
import {User} from './../models/user';

@Injectable()
export class UsersService {
    private actionUrl:string;
    private headers:Headers;

    constructor(private _http:Http) {
        this.actionUrl = CONFIGURATION.apiUrl + 'users/';

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');
    }

    public GetMe = ():Observable<User> => {
        return this._http.get(this.actionUrl + 'me')
            .map((response:Response) => <User>response.json());
    };
}
