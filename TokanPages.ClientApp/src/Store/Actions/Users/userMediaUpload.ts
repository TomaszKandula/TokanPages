import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { UploadUserMediaDto, UploadUserMediaResultDto } from "../../../Api/Models";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { UPLOAD_USER_IMAGE, RequestContract, GetConfiguration } from "../../../Api/Request";
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

//TODO: refactor, simplify
export const UserMediaUploadAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    upload:
        (payload: UploadUserMediaDto, skipDb?: boolean, handle?: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: UPLOAD });

            const content = getState().contentTemplates.content.templates.application;
            const nullError = content.nullError;
            const unexpectedStatus = content.unexpectedStatus;
            const hasBase64Data = !Validate.isEmpty(payload.base64Data);
            const hasBinaryData = !Validate.isEmpty(payload.binaryData);

            let formData = new FormData();
            if (hasBase64Data) formData.append("binaryData", payload.base64Data as string);
            if (hasBinaryData) formData.append("binaryData", payload.binaryData as File);

            const url = skipDb ? `${UPLOAD_USER_IMAGE}?skipDb=${skipDb}` : UPLOAD_USER_IMAGE;
            const headers = { "Content-Type": "multipart/form-data" };
            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: url,
                    headers: headers,
                    data: formData,
                },
            };

            axios(GetConfiguration(request))
                .then(response => {
                    if (response.status === 200) {
                        return response.data === null
                            ? RaiseError({
                                  dispatch: dispatch,
                                  errorObject: nullError,
                                  content: content,
                              })
                            : dispatch({ type: RESPONSE, payload: response.data, handle: handle });
                    }

                    const statusText = unexpectedStatus.replace("{STATUS_CODE}", response.status.toString());
                    RaiseError({
                        dispatch: dispatch,
                        errorObject: statusText,
                        content: content,
                    });
                })
                .catch(error => {
                    RaiseError({
                        dispatch: dispatch,
                        errorObject: error,
                        content: content,
                    });
                });
        },
};
