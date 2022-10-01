import axios from "axios";
import { RaiseError } from "../../../../Shared/Services/ErrorServices";
import { GetTextStatusCode } from "../../../../Shared/Services/Utilities";
import { EnrichConfiguration } from "../../../../Api/Request";
import { NULL_RESPONSE_ERROR } from "../../../../Shared/constants";
import Validate from "validate.js";

interface IGetContentAction 
{
    dispatch: any; 
    state: any; 
    request: string; 
    receive: string;
    url: string;
}

export const GetContent = (props: IGetContentAction) => 
{
    props.dispatch({ type: props.request });

    const id = props.state().applicationLanguage.id as string;
    const queryParam = Validate.isEmpty(id) ? "" : `&language=${id}`;

    axios(EnrichConfiguration(
    {
        method: "GET", 
        url: `${props.url}${queryParam}`,
        responseType: "json"
    }))
    .then(response =>
    {
        if (response.status === 200)
        {
            return response.data === null 
                ? RaiseError({ dispatch: props.dispatch, errorObject: NULL_RESPONSE_ERROR }) 
                : props.dispatch({ type: props.receive, payload: response.data });
        }

        RaiseError({ dispatch: props.dispatch, errorObject: GetTextStatusCode({ statusCode: response.status }) });
    })
    .catch(error =>
    {
        RaiseError({ dispatch: props.dispatch, errorObject: error });
    });
}
