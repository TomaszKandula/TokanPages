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

    await axios.get(url)
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

export const SendData = async (url: string, data: any): Promise<IPromiseResult> =>
{
    let result: IPromiseResult = GetDataDefault;

    await axios.post(url, data)
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
