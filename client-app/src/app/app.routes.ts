import { Routes } from '@angular/router';
import { PizzaListComponent } from './components/pizza/pizza-list/pizza-list.component';
import { PizzaDetailsComponent } from './components/pizza/pizza-details/pizza-details.component';
import { ToppingListComponent } from './components/topping/topping-list/topping-list.component';
import { ToppingDetailsComponent } from './components/topping/topping-details/topping-details.component';

export const routes: Routes = [
    { path: '', redirectTo: 'pizza-list', pathMatch: 'full' },
    { path: 'pizza-list', component: PizzaListComponent },
    { path: 'pizza-details', component: PizzaDetailsComponent },
    { path: 'topping-list', component: ToppingListComponent },
    { path: 'topping-details', component: ToppingDetailsComponent },
];
