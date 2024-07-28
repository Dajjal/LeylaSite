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
  lastKnownScrollPosition = 0;
  ticking = false;

  doSomething(scrollPos: number) {
    if (scrollPos > (window.innerHeight / 2)) {
      /*anime({
        targets: document.querySelector('#scroll_top'),
        translateY: -(32 + 60),
        duration: 500,
        easing: 'easeInOutExpo'
      });
    } else {
      anime({
        targets: document.querySelector('#scroll_top'),
        translateY: 0,
        duration: 500,
        easing: 'easeInOutExpo'
      });*/
    }
  }

  // noinspection DuplicatedCode
  constructor() {
    document.addEventListener("scroll", (event) => {
      this.lastKnownScrollPosition = window.scrollY;
      if (!this.ticking) {
        window.requestAnimationFrame(() => {
          this.doSomething(this.lastKnownScrollPosition);
          this.ticking = false;
        });
        this.ticking = true;
      }
    });
  }

  collapse_nav = () => {
    let check_box = document.getElementById('navigation_checkbox')
    if (check_box) {
      // @ts-ignore
      if (check_box.checked) {
        setTimeout(() => {
          // @ts-ignore
          check_box.checked = false;
        }, 200)
      }
    }
  }

  scroll_top = () => {
    window.scrollTo(0, 0);
  }

}
