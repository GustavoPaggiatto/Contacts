import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Address } from 'src/models/address';
import { LegalPerson } from 'src/models/LegalPerson';
import { Result } from 'src/models/result';
import { ConfigService } from 'src/services/configService';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-createlegal',
  templateUrl: './createlegal.component.html',
  styleUrls: ['./createlegal.component.css']
})
export class CreatelegalComponent {
  public person: LegalPerson;
  private _messager: MessageService;
  private _httpClient: HttpClient;
  private _configService: ConfigService;

  constructor(messager: MessageService, httpClient: HttpClient, config: ConfigService) {
    this.person = new LegalPerson();
    this.person.address = new Address();

    this._messager = messager;
    this._httpClient = httpClient;
    this._configService = config;
  }

  add() {
    if (this.person.companyName == null) {
      this._messager.showError("Company name was not informed.");
      return;
    }

    this.person.companyName = this.person.companyName.trim();

    if (this.person.companyName.length === 0) {
      this._messager.showError("Company name was not informed.");
      return;
    }

    if (this.person.tradeName == null) {
      this._messager.showError("Trade name was not informed.");
      return;
    }

    this.person.tradeName = this.person.tradeName.trim();

    if (this.person.tradeName.length === 0) {
      this._messager.showError("Trade name was not informed.");
      return;
    }

    if (this.person.document == null) {
      this._messager.showError("Document was not informed.");
      return;
    }

    if (this.person.document.length != 14) {
      this._messager.showError("Document must be 14 numbers.");
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
        this._configService.getBaseUrl() + "legalperson/Insert",
        this.person)
        .subscribe(result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          this.person.tradeName =
          this.person.companyName =
          this.person.document =
          this.person.addressNumber =
          this.person.addressComplement = "";
          this.person.address = new Address();
          
          this._messager.showSuccess("Contact was saved with success!", function() {
            console.log("Contact was added...");
          });
        },
          error => {
            console.log("An error occurred while 'POST' insert legal person request...");
            this._messager.showError("An error occurred while connecting to the server, please verify connections and re-try.");
          });
    });
  }
}
