import { Component, Input, OnInit } from '@angular/core';
import { empty } from 'rxjs';
import { Product } from './product';
import { ProductService } from './product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  constructor(private productservice: ProductService) { }
  allproducts : Product[]=[];
  addNewProduct = false;
  updateProduct=false;
  editableProduct : any;
  editableProductId : number;
  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productservice.getProducts().subscribe((data: any) => {
      this.allproducts = data;
    })
  }
  deleteProduct(id:number){
    this.productservice.deleteProduct(id).subscribe(res => {
         this.allproducts = this.allproducts.filter(item => item.productId !== id);
         console.log('Product deleted successfully!');
    })
  }

  receivedProduct(newProduct: Product) {
    this.allproducts.unshift(newProduct);
  }

  receivedEditedProduct(updatedProduct: Product) {
    this.allproducts=this.allproducts.filter(item => item.productId !== updatedProduct.productId);
    this.allproducts.unshift(updatedProduct);
  }

  showNewForm(){
    this.addNewProduct = true;
    this.updateProduct= false;
  }

  editProduct(id:number){
    this.updateProduct= false;
    this.addNewProduct = true;
    this.editableProductId=0;
    this.productservice.getProductById(id).subscribe(res=>{
      this.editableProduct = res as Product;
      this.editableProductId = this.editableProduct.productId;
    this.addNewProduct = false;
    this.updateProduct= true;
    })

  }
}
