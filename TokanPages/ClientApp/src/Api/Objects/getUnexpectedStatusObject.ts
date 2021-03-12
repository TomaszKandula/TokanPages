import { IApiResult } from "Api/Models"
import { GetUnexpectedStatusCode } from "Shared/Modals/messageHelper"

export const getUnexpectedStatusObject = (statusCode: number): IApiResult =>
{
    return {
        isSucceeded: false, 
        error: 
        { 
            ErrorCode: "UNEXPECTED_STATUS", 
            ErrorMessage: GetUnexpectedStatusCode(statusCode), 
            ValidationErrors: [] 
        }
    }
}
