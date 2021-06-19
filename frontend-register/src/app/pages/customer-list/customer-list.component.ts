import { getAllLifecycleHooks } from '@angular/compiler/src/lifecycle_reflector';
import { Component, OnInit } from '@angular/core';
import { UrlHandlingStrategy } from '@angular/router';
import { Customer } from 'src/app/models/customer.model';
import { CustomerService } from 'src/app/services/customer.service';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.scss']
})
export class CustomerListComponent implements OnInit {

  public customers: Customer[];

  constructor(private service: CustomerService) {
    this.customers = [];
  }

  ngOnInit(): void {
    this.getAll();
  }

  public getAll(): any {
    this.service.getAll().subscribe((customers: Customer[]) => {
      console.log(customers);
      this.customers = customers;
    });
  }
}
