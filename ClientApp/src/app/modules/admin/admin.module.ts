import {NgModule} from '@angular/core';
import {AdminComponent} from './admin.component';
import {AdminRoutingModule} from "./admin-routing.module";
import {SidebarNavComponent} from './components/sidebar-nav/sidebar-nav.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {NgZorroModule} from "../ngzorro/ng-zorro.module";


@NgModule({
  declarations: [
    AdminComponent,
    SidebarNavComponent
  ],
  imports: [
    AdminRoutingModule,
    NgZorroModule,
    ReactiveFormsModule,
    FormsModule,
  ]
})
export class AdminModule {
}
