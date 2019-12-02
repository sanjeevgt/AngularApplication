import { Component, Inject } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import {  Router  } from '@angular/router';

@Component({
    selector: 'app-authLogin',
    templateUrl: './authLogin.component.html',
})
export class authLoginComponent {

    public employees: User[];
    private baseUrl: string;
    constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router ) {
        this.baseUrl = baseUrl;
    }

    login(form: NgForm) {
        
        let credentials = JSON.stringify(form.value);


        this.http.post(this.baseUrl + 'auth/login', credentials, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            let token = (<any>response).token;
            localStorage.setItem("jwt", token);
            
            this.router.navigate(["/fetch-data"]);
        }, err => {
            //this.invalidLogin = true;
        });
    }
}
export interface User {
 
    Name: string;
    Password: string;

}
