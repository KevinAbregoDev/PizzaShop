import { Topping } from "../topping/topping.model";

export class Pizza {
    id: string = '';
    name: string = '';
    price: number = 0.00;
    toppings: Topping[] = [];
}