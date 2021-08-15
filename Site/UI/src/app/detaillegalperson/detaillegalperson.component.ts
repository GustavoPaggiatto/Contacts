import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LegalPerson } from 'src/models/LegalPerson';
import { Result, ResultContent } from 'src/models/result';
import { ConfigService } from 'src/services/configService';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-detaillegalperson',
  templateUrl: './detaillegalperson.component.html',
  styleUrls: ['./detaillegalperson.component.css']
})
export class DetaillegalpersonComponent {
  private _router: ActivatedRoute;
  private _httpClient: HttpClient;
  private _configService: ConfigService;
  private _messager: MessageService;
  person: LegalPerson;

  constructor(
    router: ActivatedRoute,
    httpClient: HttpClient,
    config: ConfigService,
    messager: MessageService
  ) {
    this._router = router;
    this._httpClient = httpClient;
    this._configService = config;
    this._messager = messager;

    let id: number = Number(this._router.snapshot.queryParamMap.get("id"));

    this._httpClient.get<ResultContent<LegalPerson>>(this._configService.getBaseUrl() + "legalperson/getbyid?id=" + id)
      .subscribe(
        result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          this.person = result.content;
          this.person.id = id;
        },
        error => {
          this._messager.showError("An error occurred while get details, please try again.");
          console.log("Error while 'GET' legal person details request.");
        }
      );
  }
}
