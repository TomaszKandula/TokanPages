import { BaseContract } from "../baseContract";

export interface PasswordFormInput extends BaseContract {
    oldPassword: string;
    newPassword: string;
    confirmPassword: string;
}
