import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, tap } from "rxjs";
import { Topping } from "./topping.model";

@Injectable({
    providedIn: 'root'
})

export class ToppingService {
    private apiUrl = 'http://localhost:5000/api/topping'
    private _toppings = new BehaviorSubject<Topping[]>([]);
    toppings$ = this._toppings.asObservable();

    constructor(private http: HttpClient) {}

    getToppings() {
        this.http.get<Topping[]>(this.apiUrl)
            .subscribe(toppings => this._toppings.next(toppings));
    }

    getTopping(id: string) {
        return this.http.get<Topping>(`${this.apiUrl}/${id}`);
    }

    createTopping(topping: Topping) {
        return this.http.post<Topping>(this.apiUrl, topping).pipe(
            tap(newTopping => {
                this._toppings.next([...this._toppings.value, newTopping]);
            })
        );
    }

    editTopping(topping: Topping) {
        return this.http.put<Topping>(this.apiUrl, topping).pipe(
            tap(updatedTopping => {
                const updatedToppings = this._toppings.value.map(t => t.id === topping.id ? updatedTopping : t);
                this._toppings.next(updatedToppings);
            })
        );
    }

    deleteTopping(id: string) {
        return this.http.delete<Topping>(`${this.apiUrl}/${id}`).pipe(
            tap(() => {
                this._toppings.next(this._toppings.value.filter(topping => topping.id !== id));
            })
        )
    }
}