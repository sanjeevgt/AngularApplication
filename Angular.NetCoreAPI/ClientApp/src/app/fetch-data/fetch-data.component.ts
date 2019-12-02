import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  //public forecasts: WeatherForecast[];
    public employees: Employee[];
    private headers = new HttpHeaders();

   

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    //http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
    //  this.forecasts = result;
    //}, error => console.error(error));
      let headers = new HttpHeaders();
      if (localStorage.jwt != null) {

       

        // headers.append('Content-Type', 'application/json');
          let authToken = localStorage.jwt;
        //  headers.append('Authorization', 'Bearer ' + authToken);

          headers = new HttpHeaders().set("Authorization", "Bearer " + authToken);
      }

      http.get<Employee[]>(baseUrl + 'employee/getEmployee', { headers: headers}).subscribe((result: Employee[]) => {
          this.employees = result;
      }, error => console.error(error));
  }
}

//interface WeatherForecast {
//  date: string;
//  temperatureC: number;
//  temperatureF: number;
//  summary: string;
//}

interface Employee {
    Employeeid: number;
    Name: string;
    LastName: string;
   
}
