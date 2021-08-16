import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreatelegalComponent } from './createlegal/createlegal.component';
import { CreatenaturalComponent } from './createnatural/createnatural.component';
import { DetaillegalpersonComponent } from './detaillegalperson/detaillegalperson.component';
import { DetailnaturalpersonComponent } from './detailnaturalperson/detailnaturalperson.component';
import { ListComponent } from './list/list.component';
import { UpdatelegalpersonComponent } from './updatelegalperson/updatelegalperson.component';
import { UpdatenaturalpersonComponent } from './updatenaturalperson/updatenaturalperson.component';


const routes: Routes = [
  {
    path: "",
    component: ListComponent
  },
  {
    path: "createlegal",
    component: CreatelegalComponent
  },
  {
    path: "createnatural",
    component: CreatenaturalComponent
  },
  {
    path: "detailnaturalperson",
    component: DetailnaturalpersonComponent
  },
  {
    path: "detaillegalperson",
    component: DetaillegalpersonComponent
  },
  {
    path: "updatelegalperson",
    component: UpdatelegalpersonComponent
  },
  {
    path: "updatenaturalperson",
    component: UpdatenaturalpersonComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
