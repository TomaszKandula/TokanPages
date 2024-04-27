import { BaseContract } from "../baseContract";

export interface SignupFormInput extends BaseContract {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    terms: boolean;
}
