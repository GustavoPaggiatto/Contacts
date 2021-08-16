import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Address } from 'src/models/address';
import { Gender, NaturalPerson } from 'src/models/naturalPerson';
import { Result, ResultContent } from 'src/models/result';
import { ConfigService } from 'src/services/configService';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-updatenaturalperson',
  templateUrl: './updatenaturalperson.component.html',
  styleUrls: ['./updatenaturalperson.component.css']
})
export class UpdatenaturalpersonComponent {

  public person: NaturalPerson;
  private _httpClient: HttpClient;
  private _configService: ConfigService;
  private _messager: MessageService;
  private _router: ActivatedRoute;

  constructor(httpClient: HttpClient, configService: ConfigService, messager: MessageService, router: ActivatedRoute) {
    this._configService = configService;
    this._httpClient = httpClient;
    this._messager = messager;
    this._router = router;

    let id: number = Number(this._router.snapshot.queryParamMap.get("id"));

    this._httpClient.get<ResultContent<NaturalPerson>>(this._configService.getBaseUrl() + "naturalperson/getbyid?id=" + id)
      .subscribe(
        result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          this.person = result.content;
          this.person.id = id;
          this.person.document = this.person.document.substr(0, 3) +
            "." +
            this.person.document.substr(3, 3) +
            "." +
            this.person.document.substr(6, 3) +
            "-" +
            this.person.document.substr(9, 2)
        },
        error => {
          this._messager.showError("An error occurred while get details, please try again.");
          console.log("Error while 'GET' legal person details request.");
        }
      );
  }

  update() {
    if (this.person.name == null) {
      this._messager.showError("Name was not informed.");
      return;
    }

    this.person.name = this.person.name.trim();

    if (this.person.name.length === 0) {
      this._messager.showError("Name was not informed.");
      return;
    }

    if (this.person.gender === Gender.Nothing) {
      this._messager.showError("Gender was not selected.");
      return;
    }

    if (this.person.birthDate == null) {
      this._messager.showError("Birthdate was not informed.");
      return;
    }

    if (this.person.document == null) {
      this._messager.showError("Document was not informed.");
      return;
    }

    if (this.person.document.length != 11) {
      this._messager.showError("Document must be 11 numbers.");
      return;
    }

    if (this.person.address.zipCode == null) {
      this._messager.showError("Zipcode was not informed.");
      return;
    }

    if (this.person.address.zipCode.replace("-", "").length != 8) {
      this._messager.showError("Zipcode must contains 8 numbers, please verify.");
      return;
    }

    this._messager.showQuestion(() => {
      this._httpClient.post<Result>(
        this._configService.getBaseUrl() + "naturalperson/update",
        {
          name: this.person.name,
          document: this.person.document,
          birthDate: this.person.birthDate,
          addressNumber: this.person.addressNumber,
          addressComplement: this.person.addressComplement,
          address: this.person.address,
          gender: (this.person.gender == Gender.Male ? 0 : 1),
          id: this.person.id
        })
        .subscribe(result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          this._messager.showSuccess("Contact was saved with success!", function () {
            console.log("Contact was added...");
          });
        },
          error => {
            console.log("An error occurred while 'POST' update natural person request...");
            this._messager.showError("An error occurred while connecting to the server, please verify connections and re-try.");
          });
    });
  }
}
