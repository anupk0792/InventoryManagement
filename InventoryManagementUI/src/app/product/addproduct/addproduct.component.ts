import { Component, EventEmitter, OnInit, Output } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Product } from '../product';
import { ProductService } from '../product.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-addproduct',
  templateUrl: './addproduct.component.html',
  styleUrls: ['./addproduct.component.css']
})
export class AddproductComponent implements OnInit {
  form!: FormGroup;
  newProduct : any;
  @Output() addedProduct = new EventEmitter<Product>();

  constructor(
    public productservice: ProductService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      productId: new FormControl('', [Validators.required]),
      name: new FormControl('', Validators.required),
      description: new FormControl('', [Validators.required]),
      category: new FormControl('', Validators.required),
      imageUrl: new FormControl('', [Validators.required]),
      price: new FormControl('', Validators.required)
    });
  }

  get f(){
    return this.form.controls;
  }

  addProduct(){
    this.productservice.addProduct(this.form.value).subscribe(res => {
         this.newProduct = res;
         this.addedProduct.emit(this.newProduct);
         this.form.reset();
    })
  }
}
