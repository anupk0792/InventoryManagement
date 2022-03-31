import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule }    from '@angular/common/http';
import { ProductComponent } from './product/product.component';
import { ProductService } from './product/product.service';
import { AddproductComponent } from './product/addproduct/addproduct.component';
import { EditproductComponent } from './product/editproduct/editproduct.component';

@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    AddproductComponent,
    EditproductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
