interface IValidationErrors
{
    PropertyName: string;
    ErrorCode: string;
    ErrorMessage: string;
}

interface IError 
{
    ErrorCode: string;
    ErrorMessage: string;
    ValidationErrors?: IValidationErrors[];
}

export type { IError }
