import { ApplicationAction } from "../../Configuration";
import { UploadUserMediaDto, UploadUserMediaResultDto } from "../../../Api/Models";
import { DispatchExecuteAction, ExecuteRequest, UPLOAD_USER_IMAGE } from "../../../Api/Request";
import Validate from "validate.js";

export const UPLOAD = "UPLOAD_USER_MEDIA";
export const CLEAR = "UPLOAD_USER_MEDIA_CLEAR";
export const RESPONSE = "UPLOAD_USER_MEDIA_RESPONSE";
interface Upload {
    type: typeof UPLOAD;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: UploadUserMediaResultDto;
    handle?: string;
}
export type TKnownActions = Upload | Clear | Response;

export const UserMediaUploadAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    upload:
        (payload: UploadUserMediaDto, skipDb?: boolean, handle?: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPLOAD });

            const hasBase64Data = !Validate.isEmpty(payload.base64Data);
            const hasBinaryData = !Validate.isEmpty(payload.binaryData);

            let formData = new FormData();
            if (hasBase64Data) formData.append("binaryData", payload.base64Data as string);
            if (hasBinaryData) formData.append("binaryData", payload.binaryData as File);

            const url = skipDb ? `${UPLOAD_USER_IMAGE}?skipDb=${skipDb}` : UPLOAD_USER_IMAGE;
            const headers = new Headers();
            headers.append("Content-Type", "multipart/form-data");
            const input: ExecuteRequest = {
                url: url,
                dispatch: dispatch,
                state: getState,
                optionalHandle: handle,
                responseType: RESPONSE,
                configuration: {
                    method: "POST",
                    headers: headers,
                    body: payload,
                },
            };

            DispatchExecuteAction(input);
        },
};
