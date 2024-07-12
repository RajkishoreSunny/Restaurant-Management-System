import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HttpClientModule, provideHttpClient, withFetch } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { MenucategoryComponent } from './menucategory/menucategory.component';
import { MenuitemsComponent } from './menuitems/menuitems.component';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MenuitemComponent } from './menuitem/menuitem.component';
import { PaymentComponent } from './payment/payment.component';
import { BooktableComponent } from './booktable/booktable.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { PastordersComponent } from './pastorders/pastorders.component';
import { AboutComponent } from './about/about.component';
import { RestaurantmanagerComponent } from './restaurantmanager/restaurantmanager.component';
import { ManagerloginComponent } from './managerlogin/managerlogin.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { AddMenuComponent } from './add-menu/add-menu.component';
import { InvoiceComponent } from './invoice/invoice.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavbarComponent,
    HomeComponent,
    MenucategoryComponent,
    MenuitemsComponent,
    MenuitemComponent,
    PaymentComponent,
    BooktableComponent,
    RegisterComponent,
    ProfileComponent,
    PastordersComponent,
    AboutComponent,
    RestaurantmanagerComponent,
    ManagerloginComponent,
    AddCategoryComponent,
    AddMenuComponent,
    InvoiceComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    RouterModule,
  ],
  providers: [
    provideClientHydration(),
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
