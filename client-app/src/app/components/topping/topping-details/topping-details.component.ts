import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { ToppingService } from "../topping.service";
import { Topping } from "../topping.model";

@Component({
    selector: 'app-topping-detail',
    templateUrl: './topping-details.component.html',
    styles: ''
})

export class ToppingDetailsComponent implements OnInit {
    topping$!: Observable<Topping>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private toppingService: ToppingService
    ) {}
    
    ngOnInit(): void {
        
    }
}