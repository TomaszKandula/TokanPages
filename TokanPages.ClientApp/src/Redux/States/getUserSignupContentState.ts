import { IUserSignupContentDto } from "../../Api/Models";

export interface IGetUserSignupContent extends IUserSignupContentDto
{
    isLoading: boolean;
}