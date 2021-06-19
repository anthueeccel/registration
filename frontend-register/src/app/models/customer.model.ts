import { GenderType } from "../enum/gender-type";

export interface Customer {
    firstName: string;
    lastName: string;
    birthdate: Date;
    gender: GenderType;
}