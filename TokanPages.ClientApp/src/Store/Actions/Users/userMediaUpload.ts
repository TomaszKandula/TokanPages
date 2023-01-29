import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { UploadUserMediaDto, UploadUserMediaResultDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { 
    UPLOAD_USER_MEDIA,
    RequestContract, 
    GetConfiguration,
} from "../../../Api/Request";

export const UPLOAD = "UPLOAD_USER_MEDIA";
export const CLEAR = "UPLOAD_USER_MEDIA_CLEAR";
export const RESPONSE = "UPLOAD_USER_MEDIA_RESPONSE";
interface Upload { type: typeof UPLOAD }
interface Clear { type: typeof CLEAR }
interface Response { type: typeof RESPONSE; payload: UploadUserMediaResultDto; handle?: string; }
export type TKnownActions = Upload | Clear | Response;

//TODO: refactor, simplify
export const UserMediaUploadAction = 
{
    clear: (): ApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    upload: (payload: UploadUserMediaDto, skipDb?: boolean, handle?: string): ApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPLOAD });

        let formData = new FormData();
        formData.append("userId", payload.userId as string);
        formData.append("mediaTarget", payload.mediaTarget.toString());
        formData.append("data", payload.data);

        const url = skipDb ? `${UPLOAD_USER_MEDIA}?skipDb=${skipDb}` : UPLOAD_USER_MEDIA;

        const request: RequestContract = {
            configuration: {
                method: "POST", 
                url: url, 
                headers: { "Content-Type": "multipart/form-data" },
                data: formData    
            }
        }

        axios(GetConfiguration(request))
        .then(response => 
        {
            if (response.status === 200)
            {
                return response.data === null 
                ? RaiseError({ dispatch: dispatch, errorObject: NULL_RESPONSE_ERROR}) 
                : dispatch({ type: RESPONSE, payload: response.data, handle: handle });
            }
            
            RaiseError({ dispatch: dispatch, errorObject: GetTextStatusCode({ statusCode: response.status })});
        })
        .catch(error => 
        {
            RaiseError({ dispatch: dispatch, errorObject: error });
        });
    }
}
