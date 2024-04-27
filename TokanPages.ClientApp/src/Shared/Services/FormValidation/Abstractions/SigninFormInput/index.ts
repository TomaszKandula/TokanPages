import { BaseContract } from "../baseContract";

export interface SigninFormInput extends BaseContract {
    email: string;
    password: string;
}
