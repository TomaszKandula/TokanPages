import axios from "axios";
import { useDispatch } from "react-redux";
import * as Sentry from "@sentry/react";
import { RAISE_ERROR } from "../Redux/Actions/raiseErrorAction";
import { GetErrorMessage } from "../Shared/helpers";

interface IPromiseResult 
{
    status: number | null;
    content: any | null;
    error: any | null;
}

export const GetDataDefault: IPromiseResult = 
{
    status: null,
    content: null,
    error: null
}

export const GetData = async (url: string): Promise<IPromiseResult> =>
{
    let result: IPromiseResult = GetDataDefault;

    await axios.get(url, { method: "GET" })
    .then(response =>
    {
        result = 
        {
            status: response.status,
            content: response.data,
            error: null
        }
    })
    .catch(error =>
    {
        result = 
        { 
            status: null,
            content: null,
            error: error 
        };
        
        const dispatch = useDispatch();
        dispatch({ type: RAISE_ERROR, errorObject: GetErrorMessage(error) });
        Sentry.captureException(error);
    });

    return result;
}
