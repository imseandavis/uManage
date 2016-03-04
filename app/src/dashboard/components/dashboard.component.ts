import {Component, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';
import {User} from './../../shared/models/user';
import {UsersService} from './../../shared/services/users.service';

@Component({
    selector: 'umanage-home',
    moduleId: module.id,
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.css'],
    providers: [UsersService],
    directives: [FORM_DIRECTIVES, CORE_DIRECTIVES]
})
export class DashboardComponent implements OnInit {
    public currentUser:User;

    constructor(private _usersService:UsersService) {
    }

    ngOnInit() {
        this.getCurrentUser();
    }

    private getCurrentUser():void {
        this._usersService
            .GetMe()
            .subscribe(data => this.currentUser = data,
                () => console.log('Got the current user!' + this.currentUser.Email));
    }
}
