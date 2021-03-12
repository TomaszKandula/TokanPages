import { IApiResult } from "Api/Models"
import { UNEXPECTED_ERROR } from "Shared/constants"

export const getUnexpectedErrorObject = (): IApiResult =>
{
    return {
        isSucceeded: false, 
        error: 
        { 
            ErrorCode: "UNEXPECTED_ERROR", 
            ErrorMessage: UNEXPECTED_ERROR, 
            ValidationErrors: [] 
        }
    }
}
