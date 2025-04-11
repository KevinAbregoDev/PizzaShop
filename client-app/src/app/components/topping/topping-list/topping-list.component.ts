import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { ToppingService } from "../topping.service";
import { Topping } from "../topping.model";

@Component({
    selector: 'app-topping-list',
    templateUrl: './topping-list.component.html',
    styles: ''
})

export class ToppingListComponent implements OnInit {
    toppings$!: Observable<Topping[]>;

    constructor(private toppingService: ToppingService) {}

    ngOnInit(): void {
        this.toppings$ = this.toppingService.toppings$;
        this.toppingService.getToppings();
    }
}