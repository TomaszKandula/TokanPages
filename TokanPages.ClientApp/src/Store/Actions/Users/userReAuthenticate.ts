import axios from "axios";
import { ApplicationAction } from "../../Configuration";
import { ReAuthenticateUserDto } from "../../../Api/Models";
import { UPDATE, TKnownActions as TUpdateActions } from "./userDataStore";
import { RaiseError } from "../../../Shared/Services/ErrorServices";
import { REAUTHENTICATE as REAUTHENTICATE_USER, RequestContract, GetConfiguration } from "../../../Api/Request";

export const REAUTHENTICATE = "REAUTHENTICATE_USER";
export const CLEAR = "REAUTHENTICATE_USER_CLEAR";
export const RESPONSE = "REAUTHENTICATE_USER_RESPONSE";
interface ReAuthenticate {
    type: typeof REAUTHENTICATE;
}
interface Clear {
    type: typeof CLEAR;
}
interface Response {
    type: typeof RESPONSE;
    payload: object;
}
export type TKnownActions = ReAuthenticate | Clear | Response | TUpdateActions;

//TODO: refactor, simplify
export const UserReAuthenticateAction = {
    clear: (): ApplicationAction<TKnownActions> => dispatch => {
        dispatch({ type: CLEAR });
    },
    reAuthenticate:
        (refreshToken: string, userId: string): ApplicationAction<TKnownActions> =>
        (dispatch, getState) => {
            dispatch({ type: REAUTHENTICATE });

            const content = getState().contentTemplates.content.templates.application;
            const nullError = content.nullError;
            const unexpectedStatus = content.unexpectedStatus;

            const payload: ReAuthenticateUserDto = {
                userId: userId,
                refreshToken: refreshToken,
            };

            const request: RequestContract = {
                configuration: {
                    method: "POST",
                    url: REAUTHENTICATE_USER,
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
