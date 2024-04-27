import { BaseContract } from "../baseContract";

export interface UpdateFormInput extends BaseContract {
    newPassword: string;
    verifyPassword: string;
}
