import {HttpClient} from '@angular/common/http';
import {Component} from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClientApp';

  public forecasts: WeatherForecast[] = [];

  constructor(private http: HttpClient) {
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/api/WeatherForecast')
      .subscribe(result => {
          console.log('result:', result);
        }
      );
  }
}
