import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Address } from 'src/models/address';
import { Gender, NaturalPerson } from 'src/models/naturalPerson';
import { Result } from 'src/models/result';
import { ConfigService } from 'src/services/configService';
import { MessageService } from 'src/services/messageService';

@Component({
  selector: 'app-createnatural',
  templateUrl: './createnatural.component.html',
  styleUrls: ['./createnatural.component.css']
})
export class CreatenaturalComponent {
  public person: NaturalPerson;
  private _httpClient: HttpClient;
  private _configService: ConfigService;
  private _messager: MessageService;

  constructor(httpClient: HttpClient, configService: ConfigService, messager: MessageService) {
    this.person = new NaturalPerson();
    this.person.address = new Address();
    this.person.gender = Gender.Nothing;

    this._configService = configService;
    this._httpClient = httpClient;
    this._messager = messager;
  }

  add() {
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
        this._configService.getBaseUrl() + "naturalperson/Insert",
        {
          name: this.person.name,
          document: this.person.document,
          birthDate: this.person.birthDate,
          addressNumber: this.person.addressNumber,
          addressComplement: this.person.addressComplement,
          address: this.person.address,
          gender: (this.person.gender == Gender.Male ? 0 : 1),
          id: 0
        })
        .subscribe(result => {
          if (result.hasError) {
            this._messager.showError(result.errors[0]);
            return;
          }

          this.person.name =
          this.person.document =
          this.person.addressNumber =
          this.person.addressComplement = "";
          this.person.birthDate = null;
          this.person.gender = -1;
          this.person.address = new Address();
          
          this._messager.showSuccess("Contact was saved with success!", function() {
            console.log("Contact was added...");
          });
        },
          error => {
            console.log("An error occurred while 'POST' insert natural person request...");
            this._messager.showError("An error occurred while connecting to the server, please verify connections and re-try.");
          });
    });
  }
}
