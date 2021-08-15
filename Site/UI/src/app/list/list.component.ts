import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { PersonDto } from 'src/models/personDto';
import { HttpClient } from '@angular/common/http';
import { ConfigService } from 'src/services/configService';
import { Result, ResultContent } from 'src/models/result';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css']
})
export class ListComponent implements AfterViewInit {
  private _http: HttpClient;
  private _config: ConfigService;
  private _messager: MessageService;
  public displayedColumns: string[] = ['Name', 'Document', 'Address', 'Options'];
  public dataSource: MatTableDataSource<PersonDto>;
  
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatTable) table: MatTable<any>;

  constructor(http: HttpClient, config: ConfigService, messager: MessageService) {
    this._config = config;
    this._http = http;
    this._messager = messager;

    this._http.get<ResultContent<PersonDto[]>>(this._config.getBaseUrl() + "legalperson/getpersons")
      .subscribe(
        result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }
          this.dataSource = new MatTableDataSource<PersonDto>(result.content);
          this.dataSource.paginator = this.paginator;
        },
        error => {
          console.log("Error while 'GET' request (view logs for more details)...");
          this._messager.showError("An error occurred in connection with server, verify internet connections.");
        });
  }

  ngAfterViewInit() {

  }

  delete(id: number) {
    this._messager.showQuestion(() => {
      this._http.post<Result>(this._config.getBaseUrl() + "legalperson/delete", {
        id: id
      }).subscribe(
        result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          let ix = this.dataSource.data.findIndex(ix => ix.id == id);
          
          this.dataSource.data.splice(ix, 1);
          this.dataSource = new MatTableDataSource<PersonDto>(this.dataSource.data);
          this.table.renderRows();
          
          this._messager.showSuccess("Contact was deleted with success!", function () {
            console.log("Contact " + id + " was deleted.");
          });
        },
        error => {
          console.log("Error while 'POST' deleting request (view logs for more details)...");
          this._messager.showError("An error occurred while deleting a contact, please try again.");
        }
      );
    });
  }
}
