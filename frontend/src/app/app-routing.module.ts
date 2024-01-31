import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CandidateListComponent } from './Pages/candidate/get/candidate-list.component';

const routes: Routes = [

  { path: '', component: CandidateListComponent, title: 'Home page' },
  
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
