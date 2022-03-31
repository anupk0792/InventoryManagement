import { Injectable } from '@angular/core';

import { HttpClient,HttpHeaders }    from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  private apiHostURL = "https://localhost:44354/api/product";

  getProducts(){
    return this.http.get(this.apiHostURL + '/getproducts');
  }

  getProductById(id:number){
    return this.http.get(this.apiHostURL + '/getproductbyid/'+id);
  }

  addProduct(formData: any){
    return this.http.post(this.apiHostURL + '/addproduct', formData);
  }

  updateProduct(formData: any){
    return this.http.put(this.apiHostURL + '/updateproduct',formData);
  }

  deleteProduct(id: number){
    return this.http.delete(this.apiHostURL + '/deleteproduct/'+id);
  }
}
