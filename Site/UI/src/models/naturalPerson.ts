import { Person } from "./person";

export class NaturalPerson extends Person {
    birthDate: Date;
    gender: Gender;
    name: string;
}

export enum Gender {
    Nothing = -1,
    Male = 0,
    Female = 1
}