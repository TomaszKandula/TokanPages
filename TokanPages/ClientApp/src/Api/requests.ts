import axios from "axios";
import { IApiRequest, IApiResult } from "./Models";
import { 
    getEmptyObject, 
    getUnexpectedErrorObject, 
    getUnexpectedStatusObject } 
from "./Objects";

const apiRequest = async (request: IApiRequest): Promise<IApiResult> =>
{
    let results: IApiResult = getUnexpectedErrorObject();

    await axios(
    { 
        method: request.method, 
        url: request.url, 
        data: request.data
    })
    .then(function (response) 
    {
        results = response.status === 200 
            ? getEmptyObject() 
            : getUnexpectedStatusObject(response.status);
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

const getDataFromUrl = async (url: string): Promise<any> =>
{
    let result: string = "";

    await axios({ method: "GET", url: url })
    .then((response) => 
    {
        result = response.status === 200 
            ? response.data 
            : console.warn(response.status);
    })
    .catch((error) => 
    {
        console.error(error);
    });

    return result;
}

export { apiRequest, getDataFromUrl }
