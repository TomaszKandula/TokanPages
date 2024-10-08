import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { AuthenticateUserDto } from "../../../Api/Models";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { AUTHENTICATE as AUTHENTICATE_USER, RequestContract, GetConfiguration } from "../../../Api/Request";

export const SIGNIN = "SIGNIN_USER";
export const CLEAR = "SIGNIN_USER_CLEAR";
export const RESPONSE = "SIGNIN_USER_RESPONSE";
interface Signin {
    type: typeof SIGNIN;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = Signin | Clear | Response | TUpdateActions;

//TODO: refactor, simplify
export const UserSigninAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    signin:
        (payload: AuthenticateUserDto): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: SIGNIN });

            const content = getState().contentPageData.components.templates.templates.application;
            const nullError = content.nullError;
            const unexpectedStatus = content.unexpectedStatus;

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: AUTHENTICATE_USER,
                    data: payload,
                },
            };

            axios(GetConfiguration(request))
                .then(response => {
                    if (response.status === 200) {
                        const pushData = () => {
                            dispatch({ type: RESPONSE, payload: response.data });
                            dispatch({ type: UPDATE, payload: response.data });
                        };

                        return response.data === null
                            ? RaiseError({
                                  dispatch: dispatch,
                                  errorObject: nullError,
                                  content: content,
                              })
                            : pushData();
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
