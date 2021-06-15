import { IValidationErrorsDto } from "./validationErrorsDto";

export interface IErrorDto 
{
    ErrorCode: string;
    ErrorMessage: string;
    ValidationErrors?: IValidationErrorsDto[];
}
