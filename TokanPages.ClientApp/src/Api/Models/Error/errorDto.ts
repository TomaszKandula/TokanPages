import { IValidationErrorsDto } from "./validationErrorsDto";

export interface IErrorDto 
{
    errorCode: string;
    errorMessage: string;
    validationErrors?: IValidationErrorsDto[];
}
