import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { LegalPerson } from 'src/models/LegalPerson';
import { Result, ResultContent } from 'src/models/result';
import { ConfigService } from 'src/services/configService';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-updatelegalperson',
  templateUrl: './updatelegalperson.component.html',
  styleUrls: ['./updatelegalperson.component.css']
})
export class UpdatelegalpersonComponent {
  private _httpClient: HttpClient;
  private _configService: ConfigService;
  private _messageService: MessageService;
  private _router: ActivatedRoute;
  public person: LegalPerson;

  constructor(httpClient: HttpClient, configService: ConfigService, messager: MessageService, router: ActivatedRoute) {
    this._httpClient = httpClient;
    this._configService = configService;
    this._messageService = messager;
    this._router = router;

    let id: number = Number(this._router.snapshot.queryParamMap.get("id"));

    this._httpClient.get<ResultContent<LegalPerson>>(this._configService.getBaseUrl() + "legalperson/getbyid?id=" + id)
      .subscribe(
        result => {
          if (result.hasError) {
            this._messageService.showError(result.errors[0]);
            return;
          }

          this.person = result.content;
          this.person.id = id;
          this.person.document = this.person.document.substr(0, 2) +
            "." +
            this.person.document.substr(2, 3) +
            "." +
            this.person.document.substr(5, 3) +
            "/" +
            this.person.document.substr(8, 4) +
            "-" +
            this.person.document.substr(12, 2);
        },
        error => {
          this._messageService.showError("An error occurred while get details, please try again.");
          console.log("Error while 'GET' legal person details request.");
        }
      );
  }

  update() {
    if (this.person.companyName == null) {
      this._messageService.showError("Company name was not informed.");
      return;
    }

    this.person.companyName = this.person.companyName.trim();

    if (this.person.companyName.length === 0) {
      this._messageService.showError("Company name was not informed.");
      return;
    }

    if (this.person.tradeName == null) {
      this._messageService.showError("Trade name was not informed.");
      return;
    }

    this.person.tradeName = this.person.tradeName.trim();

    if (this.person.tradeName.length === 0) {
      this._messageService.showError("Trade name was not informed.");
      return;
    }

    if (this.person.document == null) {
      this._messageService.showError("Document was not informed.");
      return;
    }

    if (this.person.document.length != 14) {
      this._messageService.showError("Document must be 14 numbers.");
      return;
    }

    if (this.person.address.zipCode == null) {
      this._messageService.showError("Zipcode was not informed.");
      return;
    }

    if (this.person.address.zipCode.replace("-", "").length != 8) {
      this._messageService.showError("Zipcode must contains 8 numbers, please verify.");
      return;
    }

    this._messageService.showQuestion(() => {
      this._httpClient.post<Result>(
        this._configService.getBaseUrl() + "legalperson/update",
        this.person)
        .subscribe(result => {
          if (result.hasError) {
            this._messageService.showError(result.errors[0]);
            return;
          }
          
          this._messageService.showSuccess("Contact was saved with success!", function() {
            console.log("Contact was updated...");
            window.location.href = "";
          });
        },
          error => {
            console.log("An error occurred while 'POST' update legal person request...");
            this._messageService.showError("An error occurred while connecting to the server, please verify connections and re-try.");
          });
    });
  }
}
