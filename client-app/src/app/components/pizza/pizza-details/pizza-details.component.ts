import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable, switchMap } from "rxjs";
import { PizzaService } from "../pizza.service";
import { NgForm } from "@angular/forms";
import { Topping } from "../../topping/topping.model";
import { Pizza } from "../pizza.model";

@Component({
    selector: 'app-pizza-detail',
    templateUrl: './pizza-details.component.html',
    styles:''
})

export class PizzaDetailsComponent implements OnInit {
    pizza!: Observable<Pizza>;
    isEditMode = false;
    toppings$!: Observable<Topping[]>;
    selectedTopping!: Topping | null;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private pizzaService: PizzaService
    ) {}

    ngOnInit(): void {
        this.route.paramMap.pipe(
            switchMap(params => {
                const id = params.get('id');
                if (id) {
                    this.isEditMode = true;
                    return this.pizzaService.getPizza(id);
                } else {
                    this.isEditMode = false;
                    return new Observable<Pizza>;
                }
            })
        ).subscribe({
            next: (pizza) => {
                this.pizza.subscribe
            },
            error: (err) => {

            }
        })
    }

    onCategorySelect(topping: Topping) {
        //if(!this.pizza.toppings.some(t) => t.id === topping.id)
    }

    onSubmit(form: NgForm) {

    }
}