import axios from "axios";

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
    });

    return result;
}
