import { IApiResult } from "Api/Models"

export const getEmptyObject = (): IApiResult => 
{
    return {
        isSucceeded: true, 
        error: 
        { 
            ErrorCode: "", 
            ErrorMessage: "", 
            ValidationErrors: [] 
        }
    }
}
