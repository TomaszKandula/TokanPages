interface IErrorDto 
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

export type { IErrorDto }
