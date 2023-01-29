import { ValidationErrorsDto } from "./validationErrorsDto";

export interface ErrorDto 
{
    errorCode: string;
    errorMessage: string;
    validationErrors?: ValidationErrorsDto[];
}
