import axios from "axios";

//TODO: remove 'getDataFromUrl' method
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

export { getDataFromUrl }

interface IPromiseResult 
{
    status: number | null;
    data: any | null;
    error: any | null;
}

export const GetData = async (url: string): Promise<IPromiseResult> =>
{
    let result: IPromiseResult = 
    { 
        status: null,
        data: null,
        error: null
    };

    await axios.get(url, 
    {
        method: "GET", 
        responseType: "json"
    })
    .then(response =>
    {
        result = 
        {
            status: response.status,
            data: response.data,
            error: null
        }
    })
    .catch(error =>
    {
        result = 
        { 
            status: null,
            data: null,
            error: error 
        };
    });

    return result;
}
