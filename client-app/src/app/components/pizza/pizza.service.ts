import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, tap } from "rxjs";
import { Pizza } from "./pizza.model";

@Injectable({
    providedIn: 'root'
})

export class PizzaService {
    private apiUrl = 'http://localhost:5000/api/pizza'
    private _pizzas = new BehaviorSubject<Pizza[]>([]);
    pizzas$ = this._pizzas.asObservable();

    constructor(private http: HttpClient) {}

    getPizzas() {
        this.http.get<Pizza[]>(this.apiUrl).subscribe(pizzas => this._pizzas.next(pizzas));
    }

    getPizza(id: string) {
        return this.http.get<Pizza>(`${this.apiUrl}/${id}`).pipe(
            tap(newPizza => {
                this._pizzas.next([...this._pizzas.value, newPizza]);
            })
        );
    }

    createPizza(pizza: Pizza) {
        return this.http.post<Pizza>(this.apiUrl, pizza).pipe(
            tap(newPizza => {
                this._pizzas.next([...this._pizzas.value, newPizza]);
            })
        );
    }

    editPizza(pizza: Pizza) {
        return this.http.put<Pizza>(this.apiUrl, pizza).pipe(
            tap(updatedPizza => {
                const updatedPizzas = this._pizzas.value.map(p => p.id === pizza.id ? updatedPizza : p);
                this._pizzas.next(updatedPizzas);
            })
        );
    }

    deletePizza(id: string) {
        return this.http.delete<Pizza>(`${this.apiUrl}/${id}`).pipe(
            tap(() => {
                this._pizzas.next(this._pizzas.value.filter(pizza => pizza.id !== id));
            })
        )
    }
}