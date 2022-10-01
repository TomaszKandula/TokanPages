import { IUserSignupContentDto } from "../../../Api/Models";

export interface IContentUserSignup extends IUserSignupContentDto
{
    isLoading: boolean;
}