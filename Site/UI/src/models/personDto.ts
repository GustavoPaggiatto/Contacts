import { Address } from "./address";

export class PersonDto {
    id: number;
    name: string;
    document: string;
    suffix: string;
    address: Address;
}