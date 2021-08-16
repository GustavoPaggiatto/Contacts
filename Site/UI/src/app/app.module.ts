import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule  } from '@angular/material/input';
import { MatSelectModule  } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { ListComponent } from './list/list.component';
import { CreatelegalComponent } from './createlegal/createlegal.component';
import { CreatenaturalComponent } from './createnatural/createnatural.component';
import { DetailnaturalpersonComponent } from './detailnaturalperson/detailnaturalperson.component';
import { DetaillegalpersonComponent } from './detaillegalperson/detaillegalperson.component';
import { NgxMaskModule, IConfig } from 'ngx-mask';
import { UpdatelegalpersonComponent } from './updatelegalperson/updatelegalperson.component';
import { UpdatenaturalpersonComponent } from './updatenaturalperson/updatenaturalperson.component';

@NgModule({
  declarations: [
    AppComponent,
    ListComponent,
    CreatelegalComponent,
    CreatenaturalComponent,
    DetailnaturalpersonComponent,
    DetaillegalpersonComponent,
    UpdatelegalpersonComponent,
    UpdatenaturalpersonComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    HttpClientModule,
    FormsModule,
    MatNativeDateModule,
    MatButtonModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    NgxMaskModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
