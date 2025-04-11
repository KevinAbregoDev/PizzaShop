import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { PizzaService } from "../pizza.service";
import { AsyncPipe, NgFor } from "@angular/common";
import { Router } from "@angular/router";
import { Pizza } from "../pizza.model";

@Component({
    imports: [NgFor, AsyncPipe],
    selector: 'app-pizza-list',
    templateUrl: './pizza-list.component.html',
    styles: ''
})

export class PizzaListComponent implements OnInit {
    pizzas$!: Observable<Pizza[]>;

    constructor(private router: Router, private pizzaService: PizzaService) {}

    ngOnInit(): void {
        this.pizzas$ = this.pizzaService.pizzas$;
        this.pizzaService.getPizzas();
    }

    viewDetails(id: string) {
        this.router.navigate(['/pizza-details', id]);
    }
}