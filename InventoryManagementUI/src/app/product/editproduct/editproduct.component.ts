import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Product } from '../product';
import { ProductService } from '../product.service';
import { Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-editproduct',
  templateUrl: './editproduct.component.html',
  styleUrls: ['./editproduct.component.css']
})
export class EditproductComponent implements OnInit {

  form!: FormGroup;
  updateProduct : any;
  @Output() editedProduct = new EventEmitter<Product>();
  @Input() editableProductId : number;
  editedProductDetails: any;
  editedProductById: Product;

  constructor(private productservice: ProductService, private router: Router) {
    this.editedProductById={productId:0, name:'',category:'', description:'', imageUrl:'',price:0 }

   }

  ngOnInit(): void {

    this.productservice.getProductById(this.editableProductId).subscribe(res => {
      this.editedProductById = res as Product;
    })
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

  submitEditProduct(){
    this.productservice.updateProduct(this.form.value).subscribe(res => {
         this.updateProduct = res;
         this.editedProduct.emit(this.updateProduct);
         this.form.reset();
    })
  }

}
