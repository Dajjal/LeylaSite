import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClient, provideHttpClient} from '@angular/common/http';
import {TranslateHttpLoader} from "@ngx-translate/http-loader";
import {SharedModule} from "./modules/shared/shared.module";
import {TranslateLoader, TranslateModule} from "@ngx-translate/core";
import {registerLocaleData} from "@angular/common";
import {NZ_I18N, ru_RU} from "ng-zorro-antd/i18n";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import ru from '@angular/common/locales/ru';

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}

registerLocaleData(ru);

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    TranslateModule.forRoot({
      defaultLanguage: 'ru',
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  providers: [
    provideHttpClient(),
    {provide: NZ_I18N, useValue: ru_RU},
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
