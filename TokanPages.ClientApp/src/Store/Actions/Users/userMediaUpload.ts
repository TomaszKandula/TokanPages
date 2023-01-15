import axios from "axios";
import { IApplicationAction } from "../../Configuration";
import { IUploadUserMediaDto, IUploadUserMediaResultDto } from "../../../Api/Models";
import { NULL_RESPONSE_ERROR } from "../../../Shared/constants";
import { GetTextStatusCode } from "../../../Shared/Services/Utilities";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { 
    UPLOAD_USER_MEDIA,
    IRequest, 
    GetConfiguration,
} from "../../../Api/Request";

export const UPLOAD = "UPLOAD_USER_MEDIA";
export const CLEAR = "UPLOAD_USER_MEDIA_CLEAR";
export const RESPONSE = "UPLOAD_USER_MEDIA_RESPONSE";
interface IUpload { type: typeof UPLOAD }
interface IClear { type: typeof CLEAR }
interface IResponse { type: typeof RESPONSE; payload: IUploadUserMediaResultDto; handle?: string; }
export type TKnownActions = IUpload | IClear | IResponse;

//TODO: refactor, simplify
export const UserMediaUploadAction = 
{
    clear: (): IApplicationAction<TKnownActions> => (dispatch) =>
    {
        dispatch({ type: CLEAR });
    },
    upload: (payload: IUploadUserMediaDto, skipDb?: boolean, handle?: string): IApplicationAction<TKnownActions> => (dispatch) => 
    {
        dispatch({ type: UPLOAD });

        let formData = new FormData();
        formData.append("userId", payload.userId as string);
        formData.append("mediaTarget", payload.mediaTarget.toString());
        formData.append("data", payload.data);

        const url = skipDb ? `${UPLOAD_USER_MEDIA}?skipDb=${skipDb}` : UPLOAD_USER_MEDIA;

        const request: IRequest = {
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
