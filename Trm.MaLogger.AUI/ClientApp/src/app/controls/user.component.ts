import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-admin-user',
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<User[]>(baseUrl + 'user').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }
  ngOnInit(): void {
    // Add some code logic here
  } 
  public users: User[] = [];
}

interface User {
  Id: number;
  Name: any;
  Email: any;
}
