import { IErrorDto } from "Api/Models";

export interface IApiResult
{
    isSucceeded: boolean;
    error: IErrorDto;
}
