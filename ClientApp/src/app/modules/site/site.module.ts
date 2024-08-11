import {NgModule} from '@angular/core';
import {SiteComponent} from './site.component';
import {SiteRoutingModule} from "./site-routing.module";
import {SharedModule} from "../shared/shared.module";


@NgModule({
  declarations: [
    SiteComponent
  ],
  imports: [
    SiteRoutingModule,
    SharedModule
  ]
})
export class SiteModule {
}
