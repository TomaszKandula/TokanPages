import axios, { Method } from "axios";
import { UNEXPECTED_ERROR } from "../Shared/constants";
import { GetUnexpectedStatusCode } from "../Shared/Modals/messageHelper";
import { IError } from "./Models";

export interface IRequest
{
    method: Method;
    url: string;
    data?: any;
}

export interface IResult
{
    isSucceeded: boolean;
    error: IError;
}

export default async function ApiRequest(request: IRequest): Promise<IResult>
{
    let results: IResult = 
    { 
        isSucceeded: false, 
        error: 
        { 
            ErrorCode: "UNEXPECTED_ERROR", 
            ErrorMessage: UNEXPECTED_ERROR, 
            ValidationErrors: [] 
        } 
    };

    await axios(
    { 
        method: request.method, 
        url: request.url, 
        data: request.data
    })
    .then(function (response) 
    {
        if (response.status === 200) 
        {
            results = 
            {
                isSucceeded: true, 
                error:         
                { 
                    ErrorCode: "", 
                    ErrorMessage: "", 
                    ValidationErrors: [] 
                } 
            };
        }
        else
        {
            results = 
            {
                isSucceeded: false, 
                error:  
                {
                    ErrorCode: "UNEXPECTED_STATUS", 
                    ErrorMessage: GetUnexpectedStatusCode(response.status), 
                    ValidationErrors: [] 
                }
            };
        }
    })
    .catch(function (error) 
    {
        console.error(error);
        results = 
        {
            isSucceeded: false, 
            error:  
            {
                ErrorCode: error.response.data.ErrorCode, 
                ErrorMessage: error.response.data.ErrorMessage, 
                ValidationErrors: error.response.data.ValidationErrors
            }
        };
    });
    
    return results;
}
