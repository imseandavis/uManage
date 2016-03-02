import {Component} from 'angular2/core';
import {CORE_DIRECTIVES, FORM_DIRECTIVES} from 'angular2/common';

@Component({
  selector: 'umanage-home',
  moduleId: module.id,
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  directives: [FORM_DIRECTIVES, CORE_DIRECTIVES]
})
export class DashboardComponent {}
