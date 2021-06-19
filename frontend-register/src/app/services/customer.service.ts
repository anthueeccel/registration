import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Customer } from '../models/customer.model';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  private customers: Customer[];
  private url = `${environment.apiUrl}/customer`;

  constructor(private httpClient: HttpClient) {
    this.customers = [];    
  }

  get getCustomer() {
    return this.customers;
  }

  getAll(): Observable<Customer[]> {
    return this.httpClient.get<Customer[]>(this.url)
  }

  add(customer: Customer) {
    this.loader(customer);
    this.customers.push(customer);
  }

  private loader(customer: Customer) {
    // customer.birthdate = new Date();
  }
}