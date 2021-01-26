interface IError 
{
    ErrorCode: string;
    ErrorMessage: string;
    ValidationErrors?: IValidationErrors[];
}

interface IValidationErrors
{
    PropertyName: string;
    ErrorCode: string;
    ErrorMessage: string;
}

export type { IError }
