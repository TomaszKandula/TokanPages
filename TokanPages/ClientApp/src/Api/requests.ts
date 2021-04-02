import axios from "axios";

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
