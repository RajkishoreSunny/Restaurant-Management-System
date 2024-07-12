import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { MenucategoryComponent } from './menucategory/menucategory.component';
import { MenuitemsComponent } from './menuitems/menuitems.component';
import { MenuitemComponent } from './menuitem/menuitem.component';
import { PaymentComponent } from './payment/payment.component';
import { BooktableComponent } from './booktable/booktable.component';
import { RegisterComponent } from './register/register.component';
import { ProfileComponent } from './profile/profile.component';
import { PastordersComponent } from './pastorders/pastorders.component';
import { AboutComponent } from './about/about.component';
import { RestaurantmanagerComponent } from './restaurantmanager/restaurantmanager.component';
import { ManagerloginComponent } from './managerlogin/managerlogin.component';
import { authGuard } from './services/auth.guard';
import { AddCategoryComponent } from './add-category/add-category.component';
import { AddMenuComponent } from './add-menu/add-menu.component';
import { InvoiceComponent } from './invoice/invoice.component';


const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "", component: HomeComponent },
  { path: "menucategory", component: MenucategoryComponent },
  { path: "menuitem", component: MenuitemsComponent },
  { path: "menuitem/:id", component:MenuitemComponent },
  { path: "payment", component: PaymentComponent },
  { path: "booktable", component: BooktableComponent },
  { path: "register", component: RegisterComponent },
  { path: "profile", component: ProfileComponent },
  { path: "pastorders", component: PastordersComponent },
  { path:"about", component: AboutComponent },
  { path: "mlogin", component: ManagerloginComponent },
  { path: "rmanager", component: RestaurantmanagerComponent , canActivate: [authGuard]},
  { path: "addcategory", component: AddCategoryComponent, canActivate: [authGuard] },
  { path: "addmenu", component: AddMenuComponent, canActivate: [authGuard] },
  { path: "invoice", component: InvoiceComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
